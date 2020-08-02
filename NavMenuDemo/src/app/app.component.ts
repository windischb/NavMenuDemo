import { Component } from '@angular/core';
import { NavMenuService } from './nav-menu.service';
import { IActionMapping, TREE_ACTIONS, TreeModel } from 'angular-tree-component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    isCollapsed = false;

    navMenu$ = this.navMenuService.GetNavMenu();


    constructor(private navMenuService: NavMenuService) {

    }

    actionMapping: IActionMapping = {
        mouse: {
            dblClick: (tree, node, $event) => {
                TREE_ACTIONS.TOGGLE_EXPANDED(tree, node, $event);
            }
        }
    };

    options = {
        idField: "Id",
        displayField: "Label",
        childrenField: "Children",
        actionMapping: this.actionMapping,
        levelPadding: 10
    };

    clickFolderExpander(node: any, $event: MouseEvent) {
        $event.stopPropagation();
        $event.preventDefault();
        node.toggleExpanded();
    }

    onMoveNode(event) {
        const tm = event.treeModel as TreeModel;
        console.log(tm.nodes)
        this.navMenuService.StoreNavMenu(tm.nodes).subscribe();
    }
}

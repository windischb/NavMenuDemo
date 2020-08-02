import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class NavMenuService {

    constructor(private http: HttpClient) { }

    GetNavMenu() {

        return this.http.get(`/navmenu${window.location.search}`);
    }

    StoreNavMenu(navMenu) {
        return this.http.post(`/navmenu`, navMenu);
    }
}
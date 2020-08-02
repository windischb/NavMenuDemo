# Navigation Menu Demo

This Repository contains a Proof of Concepot for a simple `Dynamic Navigation Menu`.  
Depending on the Modules which are registered (modules.json) and the User Roles, a Menu will be generated.  
The User can modify the Order and/or nesting of the Navigation Items from the WebClient.  
Custom Order of Navigation Items is stored in a JSON file.
If a Module is added or removed, the custom User Navigation is also updated.


# Try the Demo

* Clone the Repository
* Open the Solution
* Start Debugging or Start without Debugging...

The compiled WebClient is in the wwwroot Folder, no more Steps necessary to run the Demo.  
Just open the Browser, and navigate to `https://localhost:5001`

As this is just a Demo you do not need any User Credentials or something similar.
If nothing other is provided, the fake User has only the `Customer` Role.

In the modules.json File, there are currently 3 Roles which can be used for testing.  
Admin, Customer, Reporter

To fake other Role/Roles than `Customer` just add a QueryParameter to the URL

`https://localhost:5001?roles=admin` -> Admin Role  
`https://localhost:5001?roles=customer&roles=reporter` -> `Customer` AND `Reporter` Role

Keep in Mind, changing the Roles will update the Navigation Menu.  
A Navigation Item which is not allowed for the new Role will then be removed from the custom User Navigation!

In the user.json file the custom User Navigation Menu will be stored.  
Every time you change the Order or Role this File will be changed.


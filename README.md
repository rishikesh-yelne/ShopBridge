## ShopBridge Inventory Management

Hi! This is an Inventory Management Project created to add items to the inventory, view all inventory items, delete a particular item and also view details of a particular item. The item details include Name, Description, Price and Image. 

**Technologies Used :** ASP.NET MVC, Entity Framework, jQuery, JavaScript, Ajax, MsSQL, Unit Test, AxoCover, Rhino Mocks

**Author :** Rishikesh Yelne

## About the Application
The Home page contains two components:
1. Adding an Item
- Input the Item's Name, Description, Price and select an Image to create a new item in the inventory.
- For missing input fields, client-side and server-side validations are used. All input fields are mandatory.
- After clicking on 'Add Item', the page won't reload/refresh. Appropriate Dialog will appear to display Success/Failure.
- After successful addition of item, the list of items is reloaded.
2. List of Items
- The List contains details of the stored items : Name, Description, Price, Image
- An additional column is present to delete the particular item from the inventory.
- After clicking on 'Delete' for a particular item, a confirm dialog will appear to confirm the deletion of the item.
- If No is selected, nothing will happen. If Yes is selected, the Item will be deleted without a page reload/refresh.
- Appropriate Dialog will appear to display Success/Failure after Delete button is clicked.
- After successful deletion of item, the list of items is reloaded.
- On clicking on any item's row in the grid (all columns except the Delete column), page will be redirected to view the Details of the selected item.
- The Details page has a 'Back to Home page' link to return to the Index page.


## Pre-Requisites
1. **Database**:
	- Open Database folder.
	- Run the **Script.sql** using 'sa' login.
	The script creates a new database 'ShopBridgeDB', a new login 'sb' and a table 'itemTbl'.
	
		OR
		Restore the **ShopBridge.bak** file.
		> **Please Note** : The backup file is generated in version 14.
		
	- Accordingly update the **Connection String** 'ShopBridgeDBContext' in Web.config of MVC Project and App.config of Unit Test Project.
2. **AxoCover**:
	For Code Coverage Report, AxoCover is required as an Extension. Add the Extension using the 'Extension and Updates' feature in Visual Studio. 
	> **Please Note** : Visual Studio needs to be restarted after adding the extension.
	> **If the Report is not generated** : Please register 'OpenCover.Profiler.dll' present in the following path "C:\Users\\(UserName)\AppData\Local\Microsoft\VisualStudio\\(Version)\Extensions\\~\OpenCover\\(x86/x64)"

## Code Coverage Report
Please check the 'CodeCoverage' folder for Report Screenshot as well as Detailed Report.
	
## Tracking
1. **Backend functionality**

**Time**  : 2 hour

**Tasks** :

a. Create Database, Table.

b. Create Model (using Entity Framework - From Database).

c. Controller methods.


2. **Frontend functionality**

**Time**  : 2 hours

**Tasks** : 

a. Create 2 Main Views and required Partial Views.

b. JavaScript functions with jQuery and AJAX calls.


3. **Frontend Presentational Aspects**

**Time**  : 3 hours

**Tasks** : 

a. Adding sweetalert dialogs, font-awesome spinners on buttons, etc to improve UX

b. Center Alignment

c. Using bootstrap classes


4. **Backend-Frontend Integration**

**Time**  : 2 hours	

**Tasks** : 

a. Handling all Ajax calls from View in Controller

	
5. **Unit Testing**

**Time**  : 4 hours

**Tasks** : 

a. Testing each component during development : module-wise

b. Testing entire project with boundary cases

	
6. **Deployment and Documentation**

**Time**  : 1 hour

**Tasks** : 

a. Deploying on Git Repo

b. Documentation


**Total Time : 14 hours**

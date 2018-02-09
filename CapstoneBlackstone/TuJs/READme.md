
# TU.js
### Version 1.1.3

Temple University IS&amp;T Capstone JavaScript Library </br>
Contact the Author @ [ElGrandeQueso@temple.edu](http://tumail.temple.edu)</br>
Check the issues tab to make sure its not already being looked into</br>

**OVERVIEW:**
This Library is a compilation of functions and features that are helpful with Temple IS&T Capstone Client side features.  You are free to use it and modify it as needed.  


*SOME FEATURES USE JQUERY* version 2.2.0 (the latest version at the time of build) which is included in the zip file or downloaded [HERE] (http://jquery.com/)

*To use this Library:*
1) Download the zip file, extract it, and place the TU.js folder inside the JavaScript folder of your application (where your main pages are stored). Make sure that both JQuery.js and TU.js are inside the folder.

2) place the following code on any page that you want to use the Library. NOTE that you may need to rename the following based on if you put the library inside another directory:
```HTML
<script src="js/TU.js"></script>
```

3) To call any of the following funcitons from the library you to call the Library itself followed by the functions with any parameters that it may take.  For example, the following code will display the version function in the console when the button is clicked:

```HTML
<button onclick="TU.ver()">Click for Version</button>
```

## Usable Functions
### Below is a list of all functions in the Library:
[Calculations](#calculations)<br/>
[Table Editor](#table-editor)<br/>
[Get Size of Page](#get-size-of-page)<br/>
[Create Clock](#create-clock)<br/>
[Date Difference](#date-difference)<br/>
[Contains](#contains)<br/>
[Edit ID](#edit-id)<br/>
[Is Blank](#isblank)<br/>
[Close](#close)<br/>
[Session Timeout](#sessiontimeout)<br/>
[Add Event](#add-event)<br/>
[Remove Event](#remove-event)<br/>

#### Calculations:
###### Notes:
This function takes in 3 OR 4 arguments.
4 arguments:( First Textbox ID, Second Textbox ID, Operator as string, ID of Output Element).  Based on the Operator that you input (ie: '+'), the function will get the integer value of the two textbox inputs and perform the calculation returning the caclulated value.
3 arguments:( First Textbox ID, Second Textbox ID, Operator as string).  Does the same calculation as with 4 arguements, but the function is now assignable. ie:
```JavaScript
var ans = TU.Calc('txtbx1', 'txtbx2', '*');
```
###### INPUT:
4 Arguments (String, String, String, String) OR 3 Arguments (String, String, String)
###### OUTPUT:
can be returned as an float to perform more calculations or returns nothing and can be used to assign the value of the 4th element passed in

#### Table Editor:
###### Notes:
The table editor is a useful set of functions that help manipulate any tables or gridviews on client side.  Because of the amount of things you can do with this function it is now a dedicated function "class".
to use any of the functions, you must first call the library (TU) followed by the instantiation of the table class (tableController()) then call the method.  

###### INPUT: There are currently 4 methods for the table class:
1) ColumnToArray(string TableName, int columnNumber);
```JavaScript
var x = TU.tableController().ColumnToArray('tableName', columnNumber);
```
2) RowToArray(string TableName, string element/literal, int column Number);
```JavaScript
var x = TU.tableController().RowToArray('tableName','info', columnNumber);
```
3) SelectAllCheckBoxes(Checkbox, string Table);<br/>
This function selects/deselects all checkboxes based on the header checkbox value. The following is how your Gridview should be created allowing your head Checkbox to call the function on click
```HTML
<asp:TemplateField>
<HeaderTemplate>
  <asp:CheckBox ID="chkboxSelectAll" onclick="TU.tableController().SelectAllCheckBoxes(this, 'gridViewOrTableID');"/>
</HeaderTemplate>
<ItemTemplate>
  <asp:CheckBox ID="chkbx" runat="server"></asp:CheckBox>
</ItemTemplate>
</asp:TemplateField>
```
4) SearchGridview(input Textbox, Table Name, Column of where you are searching);
```JavaScript
var x = TU.tableController().SearchGridview(this (input object), table name, columnNumber);
```
###### OUTPUT:
1) Array containing strings of the contents in that column

2) Array containing strings of the contents in the row.  the second input can either be a string of an element(ie Textbox), or it can be a literal string for text you are searching for (ie "John")

3) NOTE** this function manipulates the table itself and filters itself on client side.  There is no postback.

#### Get Size of Page:
###### Notes:
function to give you a 2 element array containing the height and width of the page
```JavaScript
TU.getWindowSize();
```
###### INPUT:
nothing
###### OUTPUT:
two element array with the number of the height and width in pixels

#### Create Clock:
###### Notes:
This is just a simple function that will turn an elements inner HTML into a working clock.
```JavaScript
TU.createClock(element);
```
###### INPUT:
just the element you are converting into a clock
###### OUTPUT:
nothing. the element you call turns into a working clock

#### Date Difference:
###### Notes:
This function takes in 2 or 3 arguments and calculates the difference in days between the two objects
```JavaScript
TU.DateDifference = function(date1, date2, outputElement);
```
###### INPUT:
3 inputs. date1, date2, 'optionally: element you want to display'
###### OUTPUT:
if you pass in 3 arguments it returns nothing and assigns the return to the third element.  if you pass in 2 arguments it returns the value of the calculation

#### Contains:
######Notes:
this simple function takes in two arguments and returns if the element is inside the object you pass
```JavaScript
TU.Contains(container, elementYoureLookingFor);
```
###### INPUT:
2 arguments. the first is the id of the object or object itself of the container. the second is what you are looking for
###### OUTPUT:
boolean

[Back to top](#usable-functions)

#### Edit ID:
###### Notes:
This function rewrites all IDs with a given class name by appending the id+1.  very useful with dynamically created content
```JavaScript
TU.AddNumbers(classID);
```
###### INPUT:
class id
###### OUTPUT:
nothing.  the IDs will be changed client side

[Back to top](#usable-functions)

#### IsBlank:
###### Notes:
this function will return a boolean value if the element is empty or not including blank space
```JavaScript
TU.isBlank(elem);
```
###### INPUT:
element
###### OUTPUT:
boolean

[Back to top](#usable-functions)

#### Close:
###### Notes:
Calling this function will close the current browser
```JavaScript
TU.CloseWindow();
```
###### INPUT:
none
###### OUTPUT:
current window will close

[Back to top](#usable-functions)

#### SessionTimeOut:
###### Notes:
this function takes in a time and a url and if it reaches idol time. it will redirect to the given page
```JavaScript
TU.SessionTimeOut(wait, redirect);
```
###### INPUT:
time, and a URL
###### OUTPUT:
none

[Back to top](#usable-functions)

#### Add Event:
###### Notes:
this function adds an event to the given object
```JavaScript
TU.addEvent(obj, type, func);
```
###### INPUT:
takes in three parameters.  the first is the object that the event is to be added to. The second is the type of event, and the third is the function that you want called when the event triggers.
###### OUTPUT:
none, the event is added to the object passed in

[Back to top](#usable-functions)

#### Remove Event:
###### Notes:
this function removes an event to the given object
```JavaScript
TU.removeEvent(obj, type, func);
```
###### INPUT:
takes in three parameters.  the first is the object that the event is to be removed from. The second is the type of event, and the third is the function that was to be called when the event triggers.
###### OUTPUT:
none, the event is removed from the object passed in

[Back to top](#usable-functions)

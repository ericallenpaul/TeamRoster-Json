# TeamRoster w/ Json Data
TeamRoster w/ Json Data is a Console app demonstrating treating flat files as a database.

I have created 3 projects in the solution.
This will aid in the proper sepearation of 
concerns:

- TeamRoster.App
- TeamRoster.Models
- TeamRoster.Services

App will be the actual console app that 
an end-user would use. Models will be 
all of the classes we want the 
app to manage. Services will contain 
all of the methods we use to manage our data.

I am using .Net core for the main app so that once 
finished, the app can run on multiple platofrms.
I created the first project by right-clicking the
solution and choosing Add->New Project

![Add New Project](AddNewProject.bmp)

> Note: The screen above is from Visual Studio 2017. It may look a bit different depending on your version of Visual Studio.

Both the models and services projects are just class files.
These projects become DLLs and don't have a UI or anyway 
to interact with a user directly. I chose .Net Standard for
these projects. .Net Standard is generally considered 
the best choice for class libraries. I right-click the
solution and add a new project like I did with the app.

> There are some older
applications and tasks that might not be compatible so there
are some special cases when you may have to choose the 
.Net Framework version of the the class library. At the time
I'm writing this the .Net framework is only a few months
from it's last and final update from Microsoft.

![Add Models Project](AddModelsProject.bmp)

I added services project the same way.

Since I will be "persisting" the 
data between application launches 
I need a place to save our data. 
I added a folder named "Data" to the APP project.

![New Folder](new_folder.png)

Next I right-clicked the new data folder and add two new 
files (Add->New-Item) to hold the data for our classes:

- Player.json
- Team.json

![Add File](add_file.bmp)

The reason we add these empty files is so we can include the folder in the 
directory with our EXE. Now we need to click on each file and change it's output options in the properties panel.

![Copy If Newer](copy_if_newer.bmp)

Since these are going to act as my database I am selecting the 
"Copy if newer" option. This will now cause the Data folder to be included 
alnongside our exe when we run or publish the app.

Now I'll add my classes to my models project. Again this is 
a right-click but this time on the project 
instead of the solution.. 

![Add Player Class](AddPlayerClass.bmp)

Then I repeat the process to add my team class.
Now let's add some properties to both classes.
I'm adding the following code for my classes.

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TeamRoster.Models
{
    public class Player
    {
        public int Player_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public int Age { get; set; }
        public DateTime DateAdded { get; set; } 
    }
}
```
```
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamRoster.Models
{
    public class Team
    {
        public int Team_Id { get; set; }
        public string TeamName { get; set; }
    }
}
```

In a typical business aplication the next step would 
be defining the relationship between objects, 
but I'm keeping this example very simple.
Defining the relationships may also help us uncover
additional properties that we need to add to our classes.
> If you want to check out a project that includes
some database stuff then have a 
look at https://github.com/ericallenpaul/MVC-Demo

Now I'll move on to my services. The seperation here 
between models and services is my own personal preference.
For me models is a generic class library that only 
defines the ojects being used in my applications while 
services becomes things I do *WITH* the models.
Also Services does not have a one to one relationship
with models. So I can have extra services that don't
have correspondiong models. So I'm creating the matching 
services with methods like `GetAll`, `Add()`, `Edit()`, `Save()`, 
`Delete()`. Since we're using JSON data I need to add 
the nuget package that helps us deal with json data, 
Newtonsoft's JSON.Net. In the package manger console I 
select the service project and type: 
`Install-Package Newtonsoft.Json`

![Nuget Newtonsoft](nuget_newtonsoft.bmp)

We will be using the JSON.Net library to **serialize** and 
**deserialize** the data for the applications. Those are really
just fancy words for converting our objects to text and back.
Serializng our object list looks something like this:

```
//create a list of players
List<Player> players = new List<Player>();

//turn the list of playes into a json formatted string
string jsonData = JsonConvert.SerializeObject(players);

//now we can save the string to a file
File.WriteAllText(@"C:\data\MyPlayerData.json", text);
```
The "serialized" list might looks something like this:

```
[{
	"Player_Id": 1,
	"FirstName": "Luke",
	"LastName": "Skywalker",
	"Team": Rebels,
	"Age": 34,
	"DateAdded": "2019-06-18T14:52:32.9260119-04:00"
}]
```

To get our data back into the object list we just reverse the process:

```
//read the file
string jsonData = File.ReadAllText(_DataFile);

//deserialize the file back into a list
List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonData);
```

So now I have my objects (Models) and the services to create, add and delete them. Now it's
time to work on the user interface.
A console application has very limited options when it comes to a user interface.
Most applications use a menu and prompts, so the next thing to do is build a menu.
I'm going to organize my menu code by putting them all in the same folder. I created a 
folder called `Menus`. To that folder I added 3 class files:

- MainMenu.cs
- PlayerMenu.cs
- TeamMenu.cs

Each menu class will contain a `Run()` and `DisplayMenu()` method. To display the menu is
pretty straight forward. I just need a bunch of `Console.WriteLine()` methods
and the text that I want to appear on screen. So the `DisplayMenu()` method for the
main menu looks something like:

```
public static int DisplayMenu()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine("Team Roster Manager");
    Console.WriteLine("--------------------");
    Console.WriteLine();
    Console.WriteLine(" 1. Manage Teams");
    Console.WriteLine(" 2. Manage Players");
    Console.WriteLine(" 3. Exit");
    Console.WriteLine();
    Console.Write("Choice: ");
    var result = Console.ReadLine();
    return Convert.ToInt32(result);
}
```
This method is also used to return the number hat the user selected.
It has a return type of `int`.


> more instructions to come




### Code Challenge 1
There seem to be some issues when entering a player's age. Make age a 
required field and make sure it contains a value other than 0.

### Code Challenge 2
The code for the Team menu is incomplete. Finish the app 
so Team can be added like player.


### Bonus Challenge
Think about the app, is the functionality complete? We have Add and Delete
but shouldn't we also have Edit?
How hard would it be to add a new function like Edit?
How hard would it be to add a new class like "Coach"?
What if the time the various messages are on screen need to be longer than a second?
What if the time the message is on the screen needs to wait until a user 
is ready to move on to the next screen.










# Visual vector object editor (en)


## General Description
This project is a laboratory work on the OOP.

![image](https://drive.google.com/uc?export=view&id=19kfsc5QQj-JfK20p9VMZynXg8ACToO0v)
![image](https://drive.google.com/uc?export=view&id=1xbVBiExJO03yQuKkfp3BRS0H4nhc-fvl)

Here is the assignment for this laboratory work:

Implement a simple visual editor of vector objects (circle, square, triangle, segment, etc.) with the following functionality:
* Create graphical objects on the screen:
    * menu, toolbar with available object classes
    * Adding a new object to the workspace
* Manipulating objects in the workspace:
    * selection of an object to manipulate (highlighted by a color or a frame)
    * several objects selected simultaneously
    * change of color, size, position
    * deleting the object from the workspace
    * control of object moving outside the workspace (the object must not move out of the workspace boundaries with any of its parts)
* Software requirements:
    * manipulation of objects is performed using the keyboard
    * use of a proprietary object storage
    * separation of interaction with the user from the logic of the classes
* Grouping, ungrouping of objects and groups of objects (grouped objects can only be changed together). Grouping of objects must be implemented using the **Composite** pattern:
    * selecting several objects in the workspace and grouping them (with removal from the repository and placing them into a special object of the Group class, which is then placed back into the repository);
    * a group behaves as a single object: it moves, keeps the relative position of its constituent objects, and does not go beyond its boundaries;
    * a group may include other groups, etc.
* Implementation of saving and restoring from the storage using the **Abstract Factory or Factory Method pattern**:
    * saving all storage objects to a (human-readable) text file
* Restoring of all storage objects from a text file
* Add TreeView object to the application form, which will display the current storage contents
* Implement synchronization of TreeView object with the repository using the **Observer** pattern, the synchronization should be performed in both directions: when selecting an object in the tree it should be selected in the working area and vice versa, when selecting an object in the working area it should be selected in the tree
* Implement with the **Observer** pattern a special "sticky" view of the object: when you touch/cross it, other objects "stick" to it and when you move the "sticky" object, the "sticky" objects move together with it.


## Patterns used
* The **Singleton** pattern: was used in the BankOfIds class. This class is responsible for accounting for all id of graphical objects
* Pattern **Observer**: using this pattern, "sticking" of graphical objects by touch was implemented
* Pattern **Composite**: using this pattern, interaction between all kinds of graphical objects is implemented
* **Abstract Factory or Factory method pattern**: creation of all graphical objects is implemented using this pattern


## Development platform
* Windows Forms


## Supported platforms
* Windows


## Useful links
* [Project build](https://drive.google.com/drive/folders/1-oSrbRrqyOhBKvGq9fKAUHKse-vptjCQ?usp=share_link)


---


# Визуальный редактор векторных объектов (rus)


## Общее описание
Данный проект является лабораторной работой по ООП. 

![image](https://drive.google.com/uc?export=view&id=19kfsc5QQj-JfK20p9VMZynXg8ACToO0v)
![image](https://drive.google.com/uc?export=view&id=1xbVBiExJO03yQuKkfp3BRS0H4nhc-fvl)

Вот само задание к данной лабораторной работе:

Реализовать простейший визуальный редактор векторных объектов (круг, квадрат, треугольник, отрезок, и т.д.) со следующей функциональностью:
* Создание графических объектов на экране:
    * меню, панель инструментов с доступными классами объектов
    * добавление нового объекта в рабочую область
*  Манипуляции объектами в рабочей области:
    * выбор объекта для манипулирования (выделяется цветом или рамкой)
    * несколько одновременно выбранных объектов
    * изменение цвета, размера, положения
    * удаление объекта из рабочей области
    * контроль выхода за рабочую область (при передвижении объект не должен выходить за границы ни одной своей частью)
* Программные требования:
    * манипуляции объектами выполняются с помощью клавиатуры
    * использование собственного хранилища объектов
    * отделение взаимодействия с пользователем от логики работы классов
* Группировка, разгруппировка объектов и групп объектов (сгруппированные объекты изменяются только совместно). Реализация группировки объектов должна быть выполнена с помощью паттерна **Composite**:
    * выделение нескольких объектов в рабочей области и их группировка (с изъятием из хранилища и помещением в специальный объект класса Group, который затем обратно помещается в хранилище);
    * группа ведет себя как единый объект: перемещается, сохраняет относительное положение входящих в нее объектов, не выходит за границы;
    * в группу могут входить другие группы, и т.д.
* Реализация сохранения и восстановления из хранилища с помощью паттерна **Abstract Factory или Factory Method**:
    * сохранение всех объектов хранилища в (человеко-читаемый) текстовый файл
    * восстановление всех объектов хранилища из текстового файла
* Добавить на форму приложения объект TreeView для отображения текущего содержания хранилища
* Реализовать синхронизацию объекта TreeView с хранилищем с помощью паттерна **Observer**, при этом должна выполняться синхронизация в обоих направлениях: при выборе объекта в дереве он должен выбираться в рабочей области и наоборот, при выборе объекта в рабочей области он должен выбираться в дереве.
* Реализовать с помощью паттерна **Observer** специальный «липкий» вид объекта: при касании/пересечении которого другие объекты «приклеиваются» к нему и при перемещении «липкого» объекта, вместе с ним перемещаются и «приклеенные» объекты.


## Использованные паттерны
* Паттерн **Singleton**: использовался в классе BankOfIds. Этот класс отвечает за учет всех id графических объектов 
* Паттерн **Observer**: с помощью этого паттерна реализовано "прилипание" графических объектов при прикосновении
* Паттерн **Composite**: с помощью этого паттерна реализовано взаимодействие между всеми видами графических объектов 
* Паттерн **Abstract Factory** или **Factory method**: с помощью этого паттерна реализовано создание всех графических объектов 


## Платформа разработки
* Windows Forms


## Поддерживаемые платформы
* Windows


## Полезные ссылки
* [Билд проекта](https://drive.google.com/drive/folders/1-oSrbRrqyOhBKvGq9fKAUHKse-vptjCQ?usp=share_link)
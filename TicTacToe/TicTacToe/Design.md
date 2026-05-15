# TicTacToe Design

## Goal

TicTacToe-ը կոնսոլային խաղ է, որտեղ երկու կողմերը հերթով նշան են դնում 3×3 խաղադաշտի բջիջների մեջ։

Խաղի նպատակն է առաջինը կազմել երեք նույն նշաններից բաղկացած գիծ՝

* հորիզոնական,
* ուղղահայաց,
* անկյունագծով։

Այս նախագծում խաղը պետք է ունենա ոչ միայն խաղային logic, այլ նաև ամբողջական console navigation համակարգ։ Navigation-ի համար պետք է օգտագործվի առանձին `MenuLib` գրադարանը։

Ծրագրի հիմնական նպատակներն են․

* իրականացնել console TicTacToe խաղ,
* պահպանել OOP կառուցվածք,
* առանձնացնել խաղի logic-ը menu/navigation logic-ից,
* օգտագործել `MenuLib` գրադարանը էկրանների և մենյուների համար,
* չօգտագործել `System.Collections.Generic` հավաքածուներ,
* օգտագործել միայն պարզ զանգվածներ՝ `T[]`։

---

## How TicTacToe Uses MenuLib

TicTacToe նախագիծը պետք է օգտագործի `MenuLib`-ը միայն navigation-ի և console UI-ի համար։

Այսինքն՝ TicTacToe-ը չպետք է իր ներսում ունենա generic menu system։ Այդ պատասխանատվությունը պատկանում է `MenuLib`-ին։

TicTacToe-ը օգտագործում է հետևյալ MenuLib component-ները․

### `IScreen`

Օգտագործվում է յուրաքանչյուր էկրանի համար։

Օրինակ․

* `UsernameScreen`
* `MainMenuScreen`
* `PlayMenuScreen`
* `SymbolSelectionScreen`
* `GameScreen`
* `SettingsScreen`
* `AboutScreen`

Յուրաքանչյուր screen պետք է իմանա՝

* ինչպես իրեն նկարել console-ում,
* ինչպես մշակել input-ը,
* երբ պետք է անցնել ուրիշ screen։

### `MenuScreen`

Օգտագործվում է այն էկրանների համար, որտեղ user-ը ընտրում է menu item։

Օրինակ․

* Main Menu
* Play Menu
* Symbol Selection

### `MenuItem`

Օգտագործվում է menu-ի յուրաքանչյուր տարբերակը ներկայացնելու համար։

Օրինակ Main Menu-ում․

* Play
* Settings
* About
* Quit

Յուրաքանչյուր `MenuItem` ունի label և գործողություն, որը կատարվում է ընտրելուց հետո։

### `NavigationManager`

Օգտագործվում է screen-ների միջև անցումները կառավարելու համար։

Օրինակ․

* Username Screen → Main Menu
* Main Menu → Play Menu
* Play Menu → Symbol Selection
* Symbol Selection → Game Screen
* Game Screen → Main Menu

### `InputHandler`

Օգտագործվում է keyboard input-ը կարդալու և հասկանալի command-ի վերածելու համար։

Օրինակ․

* Arrow Up → Move Up
* Arrow Down → Move Down
* Enter → Select
* Escape → Back

### `Renderer`

Օգտագործվում է console-ում text, menu, highlight և board նկարելու համար։

TicTacToe-ի screen-ները կարող են օգտագործել renderer-ը UI-ը ավելի մաքուր ձևով նկարելու համար։

---

## Screen Flow

Ծրագրի screen flow-ը սկսվում է username մուտքագրելուց։ Մինչև username-ը չմուտքագրվի, user-ը չպետք է մտնի main menu։

```text
Username Screen
      │
      ▼
Main Menu
      │
      ├── Play
      │     │
      │     ▼
      │   Play Menu
      │     │
      │     ├── Player vs Player
      │     │        │
      │     │        ▼
      │     │   Symbol Selection
      │     │        │
      │     │        ▼
      │     │     Game Screen
      │     │        │
      │     │        ▼
      │     │   Result Screen / Message
      │     │        │
      │     │        ▼
      │     │     Main Menu
      │     │
      │     └── Player vs Computer
      │              │
      │              ▼
      │        Symbol Selection
      │              │
      │              ▼
      │          Game Screen
      │              │
      │              ▼
      │      Result Screen / Message
      │              │
      │              ▼
      │          Main Menu
      │
      ├── Settings
      │       │
      │       ▼
      │   Settings Screen
      │       │
      │       ▼
      │   Main Menu
      │
      ├── About
      │       │
      │       ▼
      │   About Screen
      │       │
      │       ▼
      │   Main Menu
      │
      └── Quit
              │
              ▼
          Exit Application
```

### Screen-ների բացատրություն

### `UsernameScreen`

Առաջին screen-ն է։ User-ը մուտքագրում է username։ Վավեր username-ից հետո navigation-ը անցնում է `MainMenuScreen`։

### `MainMenuScreen`

Հիմնական մենյուն է։ Ունի հետևյալ տարբերակները․

* Play
* Settings
* About
* Quit

### `PlayMenuScreen`

Խաղի ռեժիմի ընտրության մենյուն է։ Ունի երկու տարբերակ․

* Player vs Player
* Player vs Computer

### `SymbolSelectionScreen`

Այս screen-ում user-ը ընտրում է իր նշանը․

* X
* O

Նշանը ընտրելուց հետո սկսվում է խաղը։

### `GameScreen`

Ցուցադրում է 3×3 խաղադաշտը։ User-ը կարող է keyboard-ով շարժվել բջիջների միջև և `Enter`-ով նշան դնել ընտրված դատարկ բջիջում։

Խաղի ավարտից հետո ցուցադրվում է արդյունքը, և ծրագիրը վերադառնում է Main Menu։

### `SettingsScreen`

Թույլ է տալիս փոխել username-ը։ Username-ը պահվում է միայն memory-ում։

### `AboutScreen`

Ցուցադրում է developer-ի հիմնական տվյալները․

* անուն,
* դասընթաց,
* տարեթիվ։

---

## Game State

Game state-ը այն տվյալներն են, որոնք ծրագիրը պետք է հիշի աշխատանքի ընթացքում։

Այս տվյալները պետք է պահվեն TicTacToe նախագծում, ոչ թե MenuLib-ում։ MenuLib-ը չպետք է իմանա խաղի կանոնների կամ խաղային տվյալների մասին։

Ծրագիրը պետք է հիշի․

### Username

User-ի անունը։

Պահվում է memory-ում և կարող է փոխվել Settings screen-ից։

### Current Game Mode

Ընտրված խաղի ռեժիմը․

* Player vs Player
* Player vs Computer

### Player Symbol

User-ի ընտրած նշանը․

* X
* O

### Computer / Second Player Symbol

Եթե user-ը ընտրում է X, երկրորդ խաղացողը կամ computer-ը ստանում է O։

Եթե user-ը ընտրում է O, երկրորդ կողմը ստանում է X։

### Current Player

Որ խաղացողի հերթն է տվյալ պահին։

Օրինակ․

* Player 1
* Player 2
* Computer

### Board State

3×3 խաղադաշտի ներկա վիճակը։

Պետք է հիշվի՝ որ բջիջն է դատարկ, որտեղ կա X, որտեղ կա O։

### Selected Cell

Այն բջիջը, որի վրա cursor-ը կամ highlight-ը գտնվում է տվյալ պահին։

Պետք է հիշել․

* selected row,
* selected column։

### Game Result

Խաղի արդյունքը․

* Player 1 wins
* Player 2 wins
* Computer wins
* Draw
* Game still running

---

## Board Model

Խաղադաշտը 3×3 կառուցվածք է։ Քանի որ առաջադրանքում արգելված է օգտագործել `System.Collections.Generic`, խաղադաշտը պետք է ներկայացնել պարզ զանգվածով։

Հնարավոր մոտեցումներ․

### Տարբերակ 1 — մեկաչափ զանգված

```text
Cell[] board = new Cell[9]
```

Այս մոտեցմամբ 3×3 board-ը պահվում է 9 բջիջ ունեցող array-ի մեջ։

Բջիջների index-ները կարող են լինել այսպես․

```text
0 | 1 | 2
---------
3 | 4 | 5
---------
6 | 7 | 8
```

Եթե պետք է row/column-ից ստանալ index․

```text
index = row * 3 + column
```

Օրինակ․

```text
row = 1, column = 2
index = 1 * 3 + 2 = 5
```

### Տարբերակ 2 — երկչափ զանգված

```text
Cell[,] board = new Cell[3, 3]
```

Սա ավելի բնական է 3×3 board-ի համար, որովհետև անմիջապես աշխատում ես row և column արժեքներով։

Օրինակ․

```text
board[0, 0] | board[0, 1] | board[0, 2]
board[1, 0] | board[1, 1] | board[1, 2]
board[2, 0] | board[2, 1] | board[2, 2]
```

### Առաջարկվող տարբերակ

Սկսնակի համար ավելի հասկանալի է օգտագործել երկչափ զանգված․

```text
Cell[,] board = new Cell[3, 3]
```

Որովհետև խաղային logic-ը մտածում է row/column ձևով։

Board-ը պետք է ունենա պատասխանատվություններ․

* պահել բջիջների վիճակը,
* ստուգել՝ բջիջը դատարկ է, թե ոչ,
* տեղադրել նշան դատարկ բջիջում,
* մաքրել board-ը նոր խաղի համար,
* վերադարձնել բջիջի արժեքը rendering-ի համար։

Board-ը չպետք է զբաղվի console rendering-ով։ Դա UI-ի պատասխանատվությունն է։

---

## Player Model

Player-ը ներկայացնում է խաղացողին։

Յուրաքանչյուր player պետք է ունենա․

### Name

Խաղացողի անունը։

Օրինակ․

* user-ի username-ը,
* Player 2,
* Computer։

### Symbol

Խաղացողի նշանը․

* X
* O

### Player Type

Խաղացողի տեսակը․

* Human
* Computer

Այս տարբերակումը կարևոր է, որովհետև Human player-ի քայլը գալիս է keyboard-ից, իսկ Computer player-ի քայլը որոշվում է ծրագրի logic-ով։

Player-ը չպետք է ինքնուրույն նկարել UI կամ կառավարի navigation։ Նրա գործը միայն խաղացողի տվյալները պահելն է։

---

## Win/Draw Checking

Win checking-ը պետք է ստուգի՝ արդյոք որևէ խաղացող ունի երեք նույն նշան մեկ գծի վրա։

Հաղթանակի հնարավոր գծերը 8-ն են։

### Հորիզոնական գծեր

```text
[0,0] [0,1] [0,2]
[1,0] [1,1] [1,2]
[2,0] [2,1] [2,2]
```

### Ուղղահայաց գծեր

```text
[0,0] [1,0] [2,0]
[0,1] [1,1] [2,1]
[0,2] [1,2] [2,2]
```

### Անկյունագծեր

```text
[0,0] [1,1] [2,2]
[0,2] [1,1] [2,0]
```

Win checking-ի logic-ը պետք է աշխատի այսպես․

1. Վերցնել current player-ի symbol-ը։
2. Ստուգել բոլոր հորիզոնական գծերը։
3. Ստուգել բոլոր ուղղահայաց գծերը։
4. Ստուգել երկու անկյունագծերը։
5. Եթե որևէ գծում բոլոր 3 բջիջները ունեն նույն symbol-ը, խաղացողը հաղթել է։

Draw checking-ը պետք է աշխատի այսպես․

1. Ստուգել՝ կա՞ դատարկ բջիջ board-ի վրա։
2. Եթե դատարկ բջիջ չկա և winner չկա, խաղն ավարտվում է ոչ-ոքի։

Կարևոր է․

* Win-ը պետք է ստուգել յուրաքանչյուր քայլից հետո։
* Draw-ը պետք է ստուգել միայն եթե win չկա։
* Զբաղված բջիջի վրա նոր նշան դնել չի կարելի։

---

## Ընդհանուր Architecture

TicTacToe նախագիծը պետք է բաժանվի երկու մեծ մասի․

```text
TicTacToe
│
├── Game Logic
│   ├── Board
│   ├── Cell
│   ├── Player
│   ├── GameEngine
│   └── WinnerChecker
│
└── Screens
    ├── UsernameScreen
    ├── MainMenuScreen
    ├── PlayMenuScreen
    ├── SymbolSelectionScreen
    ├── GameScreen
    ├── SettingsScreen
    └── AboutScreen
```

`Game Logic` մասը չպետք է կախված լինի console-ից։

`Screens` մասը կարող է օգտագործել MenuLib-ը և Game Logic-ը միասին։

Այս separation-ը կարևոր է, որովհետև խաղի կանոնները և UI navigation-ը տարբեր պատասխանատվություններ են։

---

## Սահմանափակումներ

Նախագծում չպետք է օգտագործել․

* `List<T>`
* `Dictionary<TKey, TValue>`
* `Queue<T>`
* `Stack<T>`
* `HashSet<T>`
* այլ `System.Collections.Generic` տիպեր

Թույլատրված է օգտագործել․

* պարզ array-ներ՝ `T[]`
* երկչափ array՝ `T[,]`
* class-եր
* enum-ներ
* interface-ներ
* abstract class-եր, եթե դրանք օգնում են architecture-ին


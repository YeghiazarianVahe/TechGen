# MenuLib Design

## Նպատակ (Goal)

MenuLib-ի հիմնական նպատակը կոնսոլային ծրագրերի համար վերօգտագործվող մենյուի և նավիգացիայի համակարգ ստեղծելն է։

Այս գրադարանը պետք է թույլ տա․

* ստեղծել տարբեր էկրաններ (screens),
* տեղափոխվել էկրանների միջև,
* կառավարել keyboard input-ը,
* նկարել մենյուները console-ում,
* առանձնացնել navigation/UI logic-ը հիմնական բիզնես logic-ից։

MenuLib-ը չպետք է կապված լինի միայն TicTacToe խաղի հետ։
Այն պետք է հնարավոր լինի օգտագործել նաև այլ console applications-ում։

---

# Ի՞նչ խնդիր է լուծում (Core Problem It Solves)

Եթե navigation logic-ը գրվի անմիջապես խաղի կամ ծրագրի ներսում, կոդը արագ կդառնա․

* խառնված,
* դժվար ընդլայնվող,
* դժվար պահպանվող,
* կրկնվող։

Օրինակ՝ եթե յուրաքանչյուր screen ինքնուրույն կարդա ստեղները, փոխի screen-ը և նկարի UI-ը, ծրագիրը կկորցնի կառուցվածքը։

MenuLib-ը լուծում է այս խնդիրը՝ առանձնացնելով․

* navigation,
* rendering,
* input handling,
* screen management։

Այսպիսով TicTacToe նախագիծը զբաղվում է միայն խաղի logic-ով, իսկ MenuLib-ը՝ UI navigation-ով։

---

# Հիմնական Աբստրակցիաներ (Main Abstractions)

## Screen

Ներկայացնում է ծրագրի մեկ էկրան։

Օրինակ․

* Main Menu Screen
* Settings Screen
* About Screen
* Game Screen

Յուրաքանչյուր screen պետք է կարողանա․

* render անել իր UI-ը,
* մշակել input-ը,
* որոշել հաջորդ գործողությունը։

---

## MenuScreen

Screen-ի հատուկ տեսակ, որը պարունակում է menu items։

Պետք է աջակցի․

* item selection,
* highlight,
* up/down navigation,
* enter selection։

---

## MenuItem

Ներկայացնում է menu-ի մեկ տարբերակ։

Օրինակ․

* Play
* Settings
* Quit

MenuItem-ը պետք է պահի․

* title,
* action կամ navigation target։

---

## NavigationManager

Կառավարում է screen-ների միջև անցումները։

Պետք է իմանա․

* որն է current screen-ը,
* ինչպես փոխել screen-ը,
* ինչպես վերադառնալ նախորդ screen։

Սա MenuLib-ի ամենակարևոր մասերից մեկն է։

---

## InputHandler

Պատասխանատու է keyboard input կարդալու համար։

Օրինակ․

* Arrow keys,
* WASD,
* Enter,
* Escape։

Այս component-ը պետք է input-ը վերածի հասկանալի command-ների։

---

## Renderer

Պատասխանատու է console UI նկարելու համար։

Պետք է կառավարի․

* text drawing,
* cursor positioning,
* highlighted items,
* screen clearing։

---

## ApplicationLoop

ApplicationLoop-ը MenuLib-ի ներսում որպես առանձին պարտադիր class չի պահվում։

MenuLib-ը reusable library է, դրա համար այն չպետք է որոշի՝

- երբ է ծրագիրը սկսվում,
- երբ է ծրագիրը կանգնում,
- ինչ screen-ներից է կազմված կոնկրետ application-ը,
- ինչ business/game state ունի application-ը։

Այդ պատասխանատվությունը պետք է մնա կոնկրետ ծրագրի վրա, օրինակ՝ TicTacToe-ի `Program.cs`-ում։

MenuLib-ը տրամադրում է միայն անհրաժեշտ գործիքները․

- `NavigationManager`
- `InputHandler`
- `Renderer`
- `Screen`
- `MenuScreen`
- `MenuItem`

Իսկ TicTacToe application-ը իր հիմնական loop-ի մեջ օգտագործում է այդ գործիքները։

Պարզ ասած՝

MenuLib-ը չի վարում ամբողջ ծրագիրը։  
MenuLib-ը օգնում է ծրագրին կառավարել navigation-ը և console UI-ը։

---

# Նավիգացիայի Մոդել (Navigation Model)

Navigation-ը պետք է աշխատի state-based մոտեցմամբ։

Ծրագիրը ցանկացած պահին ունի միայն մեկ ակտիվ screen։

Օրինակ․

MainMenuScreen >> PlayMenuScreen >> GameScreen

NavigationManager-ը պահում է current screen-ը։

Երբ user-ը ընտրում է menu item, screen-ը խնդրում է NavigationManager-ին փոխել current screen-ը։

Օրինակ․

* Play → անցում PlayMenuScreen
* Settings → անցում SettingsScreen
* Back → վերադարձ նախորդ screen

Այս մոտեցումը թույլ է տալիս ունենալ centralized navigation system։

---

# Input Handling

Keyboard input-ը չպետք է կարդան բոլոր class-երը։

Միայն InputHandler-ն է աշխատում Console.ReadKey()-ի հետ։

Input flow-ը պետք է լինի այսպես․

Keyboard >> InputHandler >> Current Screen >> NavigationManager

Օրինակ․

User presses ↓ >> InputHandler հասկանում է՝ MoveDown >> MenuScreen-ը փոխում է selected item-ը >> Renderer-ը redraw է անում menu-ը

Այս architecture-ը թույլ է տալիս․

* փոխել input system-ը առանց screen-ները կոտրելու,
* centralized input handling ունենալ,
* ավելի մաքուր OOP architecture ստանալ։

---

# Architecture Overview

MenuLib architecture-ը կարելի է պատկերացնել այսպես․
```
ApplicationLoop
│
├── NavigationManager
│       │
│       └── Current Screen
│
├── InputHandler
│
└── Renderer
```

Screen-ները օգտագործում են․

* Renderer
* InputHandler
* NavigationManager

բայց չեն կառավարում ամբողջ application state-ը ինքնուրույն։


---

# Սահմանափակումներ (Limitations)

Այս տարբերակը նախատեսված է ուսումնական նպատակների համար։

Այն․

* աշխատում է միայն console applications-ի համար,
* չունի graphical rendering,
* չունի async input system,
* չի օգտագործում advanced collections,
* նախատեսված չէ enterprise-scale UI framework լինելու համար։

Սակայն architecture-ը բավական լավն է OOP և console UI design սովորելու համար։

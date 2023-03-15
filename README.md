# WEB API Крестики-нолики

Сервис для игры в крестики нолики реализован на ASP.NET с использованием Entity Framework.
---

Сервис предполагает использование клиентом API для отслеживания состояния игры в **крестики-нолики**. При помощи HTTP запросов можно создать игровое лобби с объявлением ID игроков, а затем делать ходы на классическом 3x3 поле. 

> Для отслеживания состояний каждой из клеток используется БД SQLite, доступ к которой осуществляется через Entity Framework

![Untitled Diagram](https://user-images.githubusercontent.com/46780629/225067198-d59d1ce8-7429-4a6f-be73-0c27b5f9505a.png)

## Информация 
| Обозначения  |  |
| ------------- | ------------- |
| X  | Для первого игрока  |
| O  | Для второго игрока  |
| Поле  | 3х3  |
| Клетки  | от 0 до 8 |


__Условия победы не отличаются__
## Использование <br />
Поле представляет из себя 3х3 матрицу, значения в которой изначально равны:
```
0 1 2 
3 4 5
6 7 8
```
Используются такие HTTP запросы как: <br />
 - HTTP POST - начать новую игру. Игра создается автоматически с пустым полем Field, нужно лишь задать значения id игроков, чтобы можно было делать выборку по игроку и сколько игр он сыграл (для большой системы).
 ```curl
 curl -X 'POST' \
  'https://localhost:7138/api/Sessions/api/StartGame?player1Id=1341511&player2Id=5122242' \
 ```
 <br />
 
 - HTTP PATCH - сделать ход в выбранном поле. Если сейчас на поле ход указанного игрока, то можно на выбранной клетке поставить знак X или O в зависимости от id игрока в этой игре.
 
  ```curl
 curl -X 'PATCH' \
  'https://localhost:7138/api/Sessions/api/MakeMove?_SessionId=2&_PlayerId=1341511&_Cell=2' \
 ```
 
 <br />
  
  - HTTP GET - получить объект поля, привязанного к запрашиваемой игре. Для того, чтобы отрисовывать интерфейс игры на телефоне или компьтере можно использовать данный http запрос. И после каждого хода соперника сравнивать с предыдущим экземляром объекта поля, основываясь на этом отрисовывать UI значения.
 
 ```curl
 curl -X 'GET' \
  'https://localhost:7138/api/Sessions?_SessionId=2' \
 ```
 <br />

ISSUES: на данный момент у меня не получить прописать в DI новый DBcontext, используемый FieldHandler, поэтому некоторая часть его функционала находится непосредственно в [NotMapped] методе контроллера, а присвавание значений происходит вручную. 

TEMP:  <br />
- Перед деплоем обязательно убрать функцию Ensure Deleted, она удаляет БД и создает заного исключетельно для тестирования.<br />
- Изменить строку подключения БД, при желании поместить ее в файл с настройками проекта.<br />

TODO: <br />
- UI реализация (Desktop) <br />
- Refactor маппинга клеток <br />
- AUTO Move <br />
- Unit tests

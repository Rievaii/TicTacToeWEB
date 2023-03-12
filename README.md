# WEB API Крестики-нолики

Сервис для игры в крестики нолики реализован на ASP.NET с использованием Entity Framework.
---

Сервис содержит в себе несколько HTTP запросов, благодаря которым можно отслеживать ходы в конкретном игровом лобби. Игра создается только тогда, когда в ней содержиться 2 игрока. Предполагается, что валидация айди просходит произвольным образом со стороны компании. 


 IFIELD interface implements field as array and isGameAvailable() and (isGameWon()- check if positions are filled with one char which causes the win) methods 
 if player1id == player2id => badrequest
 
 if session not found => NotFound();
 
 MakeMove() does not search any entities is just calls methods from IField
 
состояние клеток можно отслеживать через enum 

сложно подключить dbcontext в custom class

перегрузка: если сессия не указана писать в последнюю

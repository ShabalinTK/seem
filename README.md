<h3 align="center"> README - Инструкция по перемещению на сайте (по URL) </h3>
<h4>(FIGMA - функционал сайта) </h4>

[FIGMA](https://www.figma.com/design/CtVRXZiBbU1Q8DOMki60gN/Untitled?node-id=0-1&t=8m42QEe3jkECsqTi-1)

HOST: Для запуска проекта необходимо открыть распаковать .zip архив данного репозитория и запустить .sln файл. Перед тем, как запустить проект, убедитесь, что host 8888 свободен на вашем ПК. Если нет, то необходимо поменять Ваш хост в файле MyHttpServer\config.json -> "Port": 8888.

DATABASE: Для того чтобы все необходимые данные в сайте подгружались корректно, необходимо создать свою базу данных (для этого в данном прокте использовался Docker Desktop) с таблицами(или добавить в уже существующую), наполненными необходимой информацией, для этого используйте файл script.sql.
Для подключения соединения с базой данных воспользуйтесь тем же MyHttpServer\config.json и введите свою строку подключения в  ConnectionString _("ConnectionString": "Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd")_

Далее запустите данный проект -> появится консольное окно, говорящее об успешном запуске сервера.
Теперь в строке браузера вы можете зайти на сайт и перемещаться по его содержимому ("Figma"):

Инструкция по переходам через URL:

http://localhost:8888/movies - переход на главную страницу

http://localhost:8888/admin - переход на панель управления фильмами (Необходима регистрация на сайте)

http://localhost:8888/movie?id="" (возможность перейти на конкретный фильм по id этого фильма)

http://localhost:8888/register - переход на страницу регистрации

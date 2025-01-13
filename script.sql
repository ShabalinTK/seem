CREATE TABLE [dbo].[Comments] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (100)  NOT NULL,
    [Email]       NVARCHAR (100)  NULL,
    [CommentText] NVARCHAR (1000) NOT NULL,
    [MovieId]     INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comments_Post] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id])
);

SET IDENTITY_INSERT [dbo].[Comments] ON
INSERT INTO [dbo].[Comments] ([Id], [UserName], [Email], [CommentText], [MovieId]) VALUES (6, N'qw', N'qwe@ya.com', N'qwe', 2)
INSERT INTO [dbo].[Comments] ([Id], [UserName], [Email], [CommentText], [MovieId]) VALUES (7, N'hbjhvk', N'q@ya.com', N'khvkhvkhvkhv', 2)
INSERT INTO [dbo].[Comments] ([Id], [UserName], [Email], [CommentText], [MovieId]) VALUES (1002, N'qweqwe', N'qwe@ya.com', N'asdsad', 20)
SET IDENTITY_INSERT [dbo].[Comments] OFF

CREATE TABLE [dbo].[Directors] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (255) NOT NULL,
    [Country] NVARCHAR (100) NOT NULL,
    [Age]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

SET IDENTITY_INSERT [dbo].[Directors] ON
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (1, N'Паркер Финн', N'США', 40)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (2, N'Рубен Фляйшер', N'США', 48)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (3, N'Ричард Доннер', N'США', 91)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (4, N'Ридли Скотт', N'Великобритания', 87)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (5, N'Гильермо дель Торо', N'Мексика', 59)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (6, N'Гальдер Гастелу-Уррутия', N'Испания', 46)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (7, N'Тодд Филлипс', N'США', 54)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (9, N'Дэмиен Леоне', N'США', 42)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (10, N'Ян де Бонт', N'Нидерланды', 77)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (11, N'Пьер Коффан', N'США', 45)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (12, N'Крис Рено', N'США', 48)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (13, N'Джон Красински', N'США', 45)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (14, N'Тим Миллер', N'США', 54)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (15, N'Пит Доктер', N'США', 55)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (16, N'Роналдо Дель Кармен', N'США', 59)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (17, N'Джордж Миллер', N'Австралия', 79)
INSERT INTO [dbo].[Directors] ([Id], [Name], [Country], [Age]) VALUES (1002, N'Я', N'Unknown', 0)
SET IDENTITY_INSERT [dbo].[Directors] OFF


CREATE TABLE [dbo].[Movies] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (255) NOT NULL,
    [TitleEng]    NVARCHAR (255) NOT NULL,
    [ReleaseYear] NVARCHAR (255) NOT NULL,
    [Country]     NVARCHAR (255) NOT NULL,
    [Genre]       NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Player]      NVARCHAR (MAX) NOT NULL,
    [ImagePath]   NVARCHAR (500) NOT NULL,
    [Actors]      NVARCHAR (MAX) NOT NULL,
    [DirectorId]  INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

SET IDENTITY_INSERT [dbo].[Movies] ON
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (1, N'Улыбка', N'Smile', N'2022', N'США', N'Ужасы', N'Про что фильм «Улыбка»:
Улыбка (2022)

Роуз Коттер работает в отделении скорой психиатрической помощи и однажды сталкивается с типичной, казалось бы, пациенткой — студенткой, несколько дней назад ставшей свидетелем самоубийства преподавателя. Девушка в истеричном состоянии утверждает, что находится в здравом уме и подвергается преследованиям некой злобной сущности. А после со страшной улыбкой перерезает себе горло. С этого момента Роуз начинает видеть улыбающуюся самоубийцу в самых неподходящих местах, а её жизнь постепенно разваливается на части.', N'https://rutube.ru/play/embed/5b874e838ff5dd5afa2ab77b7e685e45', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-11/1732644165_maxresdefault.jpg', N'Сози Бэйкон, Кайл Галлнер, Джесси Ашер, Робин Вайгерт, Кэйтлин Стэйси, Кэл Пенн, Роб Морган, Джиллиан Зинсер, Джуди Рейес, Дэвид Сочет', 1)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (2, N'Веном', N'Venom', N'2018', N'США', N'Ужасы, Фантастика, Блокбастер, Боевик, Триллер', N'Про что фильм «Веном»:
Веном (2018)

Что если в один прекрасный день в тебя вселяется существо-симбиот, которое наделяет тебя сверхчеловеческими способностями? Вот только Веном – симбиот совсем недобрый, и договориться с ним невозможно. Хотя нужно ли договариваться?.. Ведь в какой-то момент ты понимаешь, что быть плохим вовсе не так уж и плохо. Так даже веселее. В мире и так слишком много супергероев! Мы – Веном!', N'https://rutube.ru/play/embed/0a407fa14563349612ceb1fc56c786d9', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2022-11/1668445069_w1500_50263893.jpg', N'Том Харди, Мишель Уильямс, Риз Ахмед, Скотт Хэйз, Рейд Скотт, Дженни Слейт, Мелора Уолтерс, Вуди Харрельсон, Пегги Лу, Малкольм С. Мюррэй', 2)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (3, N'Смертельное оружие', N'Lethal Weapon', N'1987', N'США', N'Боевик, Триллер, Зарубежный', N'Про что фильм «Смертельное оружие»:
Лос-анджелесский полицейский Мартин Риггз смел и безрассуден, он буквально лезет под пули, не боясь за свою жизнь. Семьянин Роджер Мёрто старше и осторожнее. Двое полицейских, ветераны Вьетнама, расследуют дело, связанное с подозрительным самоубийством девушки. Нить поисков приводит их к торговцам наркотиками, которые также оказываются ветеранами вьетнамской войны, сплоченными в крепкую преступную организацию.
', N'https://rutube.ru/play/embed/e677e0a172dc6143063d2b4068702e4b', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-11/1731497465_w1500_51626779.jpg', N'Мэл Гибсон, Дэнни Гловер, Гэри Бьюзи, Митчелл Райан, Том Аткинс, Дарлин Лав, Трэйси Вульф, Джеки Суонсон, Дэймон Хайнс, Эбони Смит', 3)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (4, N'Чужой', N'Alien', N'1979', N'США', N'Ужасы, Фантастика, Триллер, Зарубежный', N'Про что фильм «Чужой»:
Чужой (1979)

В далеком будущем возвращающийся на Землю грузовой космический корабль перехватывает исходящий с неизвестной планеты неопознанный сигнал. Экипаж, в соответствии с основными инструкциями, обязан найти и исследовать источник сигнала. Оказавшись на планете, астронавты повсюду обнаруживают неопознанные предметы, по виду напоминающие гигантские коконы.', N'https://rutube.ru/play/embed/37d5799de2cf4fcc86515fa9461bd296', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2022-11/1668619934_w1500_50477036.jpg', N'Сигурни Уивер, Том Скеррит, Иэн Холм, Джон Хёрт, Гарри Дин Стэнтон, Вероника Картрайт, Яфет Котто, Боладжи Бадеджо, Хелен Хортон, Эдди Пауэлл', 4)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (5, N'Хеллбой', N'Hellboy', N'2004', N'США', N'Приключения, Ужасы, Фантастика, Фэнтези, Боевик, Зарубежный', N'Про что фильм «Хеллбой»:
Хеллбой: Герой из пекла (2004)

Вторая мировая война. Нацисты терпят одно сокрушительное поражение за другим. В результате исследования оккультных ритуалов в суперсекретной лаборатории учёным, работающим на фашистский режим, удаётся поднять из небытия Демона Ада, который должен помочь переломить ход войны. Но в научную лабораторию тайно проникает отряд союзников и поворачивает это мистическое и грозное оружие против нацистов. Однако никто из участников эксперимента и не предполагает, что врата потустороннего мира после открытия окажутся неподвластны людям, и им придётся столкнуться с существами ещё более опасными и непредсказуемыми, чем Демон Ада.', N'https://rutube.ru/play/embed/d41ee5e7a8bfacd547f6f137dd294e1c', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-11/1731497208_72457-hellboy-2019-hd-wallpaper.jpg', N'Рон Перлман, Джон Хёрт, Сэльма Блэр, Руперт Эванс, Карел Роден, Джеффри Тэмбор, Даг Джонс, Брайан Стил, Ладислав Беран, Бидди Ходсон', 5)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (6, N'Платформа', N'El hoyo', N'2019', N'Испания', N'Ужасы, Фантастика, Триллер, Зарубежный', N'Про что фильм «Платформа»:
Горен соглашается на участие в некоем эксперименте и вскоре приходит в себя в почти пустой комнате уровня 48, где имеются большие прямоугольные отверстия в полу и потолке. На каждом уровне находятся два человека, а сколько всего уровней — неизвестно. Этажи связывает общий колодец, по которому раз в день опускается платформа с едой, и чем ниже находятся люди, тем меньше у них шансов поесть. Каждый месяц происходит рокировка, и обитатели верхних уровней могут оказаться в самом низу, и наоборот. Поскольку разрешалось взять с собой один предмет, Горен выбрал томик «Дон Кихота», а его сосед прихватил большой самозатачивающийся нож.', N'https://rutube.ru/play/embed/617dfc9a7d011057ba0117213f3aba3a', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-11/1731496683_scale_1200.jpg', N'Иван Массаге, Сорион Эгилеор, Антония Сан Хуан, Эмилио Буале, Александра Масангкей, Зихара Ллана, Марио Пардо, Альгис Арлаускас, Чубио Фернандес, Эрик Гуд', 6)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (7, N'Джокер', N'Joker', N'2019', N'США', N'Фантастика, Боевик, Зарубежный', N'Про что фильм «Джокер»:
Готэм, начало 1980-х годов. Комик Артур Флек живет с больной матерью, которая с детства учит его «ходить с улыбкой». Пытаясь нести в мир хорошее и дарить людям радость, Артур сталкивается с человеческой жестокостью и постепенно приходит к выводу, что этот мир получит от него не добрую улыбку, а ухмылку злодея Джокера.', N'https://rutube.ru/play/embed/2be60f14bad8b96c3ac2288e3613064c', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-11/1731496363_w1500_50263164.jpg', N'Хоакин Феникс, Роберт Де Ниро, Зази Битц, Фрэнсис Конрой, Бретт Каллен, Шей Уигэм, Билл Кэмп, Гленн Флешлер, Ли Гилл, Джош Пэйс', 7)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (8, N'Гладиатор', N'Gladiator', N'2000', N'США, Великобритания', N'Приключения, Блокбастер, Боевик, Зарубежный, Драма', N'Про что фильм «Гладиатор»:
Римская империя. Бесстрашного и благородного генерала Максимуса боготворят солдаты, а старый император Марк Аврелий безгранично доверяет ему и относится как с сыну. Однако опытный воин, готовый сразиться с любым противником в честном бою, оказывается бессильным перед коварными придворными интригами. Коммод, сын Марка Аврелия, убивает отца, который планировал сделать преемником не его, а Максимуса, и захватывает власть. Решив избавиться от опасного соперника, который к тому же отказывается присягнуть ему на верность, Коммод отдаёт приказ убить Максимуса и всю его семью. Чудом выжив, но не сумев спасти близких, Максимус попадает в плен к работорговцу, который продаёт его организатору гладиаторских боёв Проксимо. Так легендарный полководец становится гладиатором. Но вскоре ему представится шанс встретиться со своим смертельным врагом лицом к лицу.', N'https://rutube.ru/play/embed/7a2330c16ecd428ff153fcc2286aa0ba', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-11/1731496076_w1500_51493767.jpg', N'Рассел Кроу, Хоакин Феникс, Конни Нильсен, Оливер Рид, Ричард Харрис, Дерек Джекоби, Джимон Хонсу, Дэвид Скофилд, Джон Шрэпнел, Томас Арана', 4)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (9, N'Ужасающий', N'Terrifier', N'2016', N'США', N'Ужасы, Триллер, Зарубежный', N'Про что фильм «Ужасающий»:
В ночь Хеллоуина клоун-маньяк терроризирует трёх девушек, а также всех, кто попадается ему на пути.', N'https://vk.com/video_ext.php?oid=-198130115&id=456239387&hash=1054c666c9478415', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-10/1729588461_cdd9a0rir4ssk1200png.jpg', N'Дженна Кэнелл, Саманта Скаффиди, Дэвид Ховард Торнтон, Катрин Коркоран, Пуйа Мохсени, Мэтт МакАллистер, Кэти Магвайр, Джино Кафарелли, Кори Дювал, Майкл Ливи', 9)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (10, N'Гадкий я', N'Despicable Me', N'2010', N'США', N'Приключения, Полнометражный, Семейный, Комедия, Боевик, Зарубежный', N'Про что мультфильм «Гадкий я»:
Гадкий снаружи, но добрый внутри Грю намерен, тем не менее, закрепить за собой статус главного архизлодея в мире, для чего он решает выкрасть Луну при помощи созданной им армии миньонов. Дело осложняют конкуренты, вставляющие высокотехнические палки в колеса, и семейные обстоятельства в виде трех сироток, о которых Грю вынужден заботиться.', N'https://rutube.ru/play/embed/8cc186222913f2b31ada30fcc70ae1a7', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2023-01/1657781818_58-kartinkin-net-p-gadkii-ya-art-oboi-71.jpg', N'Стив Карелл, Джейсон Сигел, Расселл Брэнд, Джули Эндрюс, Уилл Арнетт, Кристен Уиг, Миранда Косгров, Дэна Гайер, Элси Фишер, Пьер Коффан', 11)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (11, N'Тихое место', N'A Quiet Place', N'2018', N'США', N'Ужасы, Фантастика, Триллер, Зарубежный, Драма', N'Про что фильм «Тихое место»:
Семья с двумя детьми живёт на отдалённой ферме. Казалось бы, жизнь этих людей совершенно не отличается от жизни других таких семей, но они живут в месте, которое наполнено ужасными монстрами, реагирующими на любой звук. Семейство разучило целый комплекс специальных жестов, которые помогают им общаться друг с другом, не издавая ни единого звука. Кроме того, каждый из членов семьи должен очень тихо передвигаться, чтобы опасные существа их не услышали. Однако дом, где живут дети, не может быть самым тихим местом на земле.', N'https://rutube.ru/play/embed/410ba4f49d127c8d5fe6a3e4a6eb5809', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2023-05/1683729010_w1500_50262092.jpg', N'Эмили Блант, Джон Красински, Миллисент Симмондс, Ноа Джуп, Кейд Вудворд, Леон Рассом, Рода Пелл', 12)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (12, N'Дэдпул', N'Deadpool', N'2016', N'США', N'Приключения, Фантастика, Блокбастер, Комедия, Боевик, Триллер, Зарубежный', N'Про что фильм «Дэдпул»:
Уэйд Уилсон — наёмник. Будучи побочным продуктом программы вооружённых сил под названием «Оружие X», Уилсон приобрёл невероятную силу, проворство и способность к исцелению. Но страшной ценой: его клеточная структура постоянно меняется, а здравомыслие сомнительно. Всё, чего хочет Уилсон, — держаться на плаву в социальной выгребной яме. Но течение в ней слишком быстрое.', N'https://rutube.ru/play/embed/ffb1bc20206242b3f4c35c125f170bc6', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2022-12/1670167892_w1500_50293490.jpg', N'Райан Рейнольдс, Морена Баккарин, Эд Скрейн, ТиДжей Миллер, Джина Карано, Брианна Хилдебранд, Стефан Капичич, Лесли Аггамс, Джед Риис, Каран Сони', 13)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (13, N'Головоломка', N'Inside Out', N'2015', N'США', N'Детский, Полнометражный, Семейный, Комедия, Зарубежный', N'Про что мультфильм «Головоломка»:
В голове у обычной 11-летней школьницы Райли живут пять базовых эмоций: Радость, Грусть, Страх, Гнев и Отвращение. Они каждый день помогают ей справляться с проблемами, руководя всеми ее поступками. Вдруг выясняется, что Райли и ее родителям предстоит переезд из небольшого уютного городка в шумный и людный мегаполис. Каждая из эмоций считает, что именно она лучше прочих знает, что нужно делать в этой непростой ситуации, и в голове у девочки наступает полный хаос.', N'https://rutube.ru/play/embed/1b45889ea7f6c0477a4e8e0ed8583e11', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-06/1719649328_w1500_50546912.jpg', N'Эми Полер, Филлис Смит, Ричард Кайнд, Билл Хейдер, Льюис Блэк, Минди Кейлинг, Кейтлин Диас, Дайан Лэйн, Кайл МакЛоклен, Пола Паундстон', 14)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (14, N'Безумный Макс', N'Mad Max', N'1979', N'Австралия, США', N'Приключения, Фантастика, Боевик, Триллер', N'Про что фильм «Безумный Макс»:
Безумный Макс (1979)

В недалёком будущем после крупной катастрофы вся жизнь сосредоточилась вдоль бесчисленных магистралей. Банда байкеров, желая рассчитаться за убитого товарища, преследует молодого полицейского Макса. Жертвой их мести становится лучший друг Макса, и теперь эта же участь грозит самому Максу и его семье.

Безумный Макс 2: Воин дороги (1981)

Катастрофа пострашнее ядерной войны постигла нашу цивилизацию. Страшный энергетический кризис парализовал города, пути сообщения — одним словом, все. За топливо теперь дерутся любыми средствами. Потому что там, где бензин, — там жизнь… Одинокий водитель Макс как раз и бороздит пустоши далекой Австралии в поисках горючего. Повсюду брошенные жилища, машины, разный скарб. Наконец Макс находит поселение, превращенное в укрепленный лагерь со складами горючего. Исхудавшие, измученные люди как могут охраняют то, что им принадлежит. И вот, в довершение всех бед, на лагерь нападают враги, свирепые панки и байкеры под предводительством некоего Гумунгуса, садиста и душегуба. И тогда Макс вступает в схватку…

Безумный Макс 3: Под куполом грома (1985)

В городе Бартертауне правит коварная властительница Энтити. Для укрепления своей власти она пытается использовать Макса, непревзойденного мастера дорожных схваток. Но теперь завораживающую череду автомобильных разборок под испепеляющим солнцем начинают сменять невероятные рукопашные бои.

Безумный Макс: Дорога ярости (2015)

Преследуемый призраками беспокойного прошлого Макс уверен, что лучший способ выжить — скитаться в одиночестве. Несмотря на это, он присоединяется к бунтарям, бегущим через всю пустыню на боевой фуре, под предводительством отчаянной Фуриосы. Они сбежали из Цитадели, страдающей от тирании Несмертного Джо, и забрали у него кое-что очень ценное. Разъярённый диктатор бросает все свои силы в погоню за мятежниками, ступая на тропу войны — дорогу ярости.

Фуриоса: Хроники Безумного Макса (2024)

История похищения юной воительницы Фуриосы из Зелёных Земель, в результате которого девушка попадает в руки орды байкеров под предводительством Военачальника Дементуса. Пробираясь через Пустошь, они натыкаются на Цитадель, которой управляет Бессмертный Джо, и пока два тирана борются за господство, Фуриосе предстоит пережить множество испытаний, пытаясь найти путь домой.

Смотреть онлайн фильм Безумный Макс все части по порядку бесплатно в хорошем качестве
', N'https://rutube.ru/play/embed/c8f7bd9f01ee7afe18fb2968d2643734', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-06/1719648664_w1500_50285321.jpg', N'Том Харди, Шарлиз Терон, Мэл Гибсон, Джоэнн Сэмюэл, Хью Кияс-Бёрн, Стив Бисли, Тим Барнс, Роджер Уорд, Лиза Альденховен, Дэвид Брэкс, Бертран Кадар, Дэвид Камерон', 15)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (15, N'Смерч', N'Twister', N'1996', N'США', N'Приключения, Боевик, Триллер', N'Про что фильм «Смерч»:
Смерч (1996)

Рушащиеся, словно карточные домики, здания, разорванные линии электропередач, поднятые в воздух автомобили и животные, гибнущие люди... Мелкими и незначительными оказываются личные неурядицы героев перед лицом разбушевавшейся стихии. В отчаянный поединок с грозным и малоизученным явлением природы вступают ученые-метеорологи.', N'https://rutube.ru/play/embed/a5527b12cbe53e0964f18f7eacd43e85', N'https://hd3.2-vse-chasti-filmov.xyz/uploads/posts/2024-08/1723633315_w1500_51817992.jpg', N'Хелен Хант, Билл Пэкстон, Кэри Элвес, Джейми Герц, Филип Сеймур Хоффман, Лоис Смит, Алан Рак, Шон Уэйлен, Скотт Томсон, Тодд Филд', 10)
INSERT INTO [dbo].[Movies] ([Id], [Title], [TitleEng], [ReleaseYear], [Country], [Genre], [Description], [Player], [ImagePath], [Actors], [DirectorId]) VALUES (20, N'фывф', N'фыв', N'2342', N'вапва', N'ывыа', N'ывавыа', N'https://rutube.ru/video/f2943488088f4f5f50cadd54ca78b4d5/?r=plemwd', N'https://avatars.mds.yandex.net/i?id=ee5aade3863943260e0a25c1733ac0e655e2de11-12500667-images-thumbs&n=13', N'ты, вы, мы', 1002)
SET IDENTITY_INSERT [dbo].[Movies] OFF


CREATE TABLE [dbo].[MovieStats] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [MovieId] INT NOT NULL,
    [UserId]  INT NOT NULL,
    [IsLike]  BIT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MovieStats_Movie] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id]),
    CONSTRAINT [FK_MovieStats_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Person] ([Id])
);

SET IDENTITY_INSERT [dbo].[MovieStats] ON
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (1, 1, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (2, 2, 1, 0)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (3, 3, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (4, 4, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (5, 5, 1, 0)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (6, 6, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (7, 7, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (8, 8, 1, 0)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (9, 9, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (10, 10, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (11, 11, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (12, 12, 1, 0)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (13, 13, 1, 1)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (14, 14, 1, 0)
INSERT INTO [dbo].[MovieStats] ([Id], [MovieId], [UserId], [IsLike]) VALUES (15, 15, 1, 1)
SET IDENTITY_INSERT [dbo].[MovieStats] OFF


CREATE TABLE [dbo].[Person] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Login]    NVARCHAR (50)  NOT NULL,
    [Password] NVARCHAR (50)  NOT NULL,
    [Email]    NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

SET IDENTITY_INSERT [dbo].[Person] ON
INSERT INTO [dbo].[Person] ([Id], [Login], [Password], [Email]) VALUES (1, N'admin', N'123', N'admin@ya.com')
INSERT INTO [dbo].[Person] ([Id], [Login], [Password], [Email]) VALUES (2, N'tim', N'12', N'tim@ya.com')
INSERT INTO [dbo].[Person] ([Id], [Login], [Password], [Email]) VALUES (3, N'Tam', N'123', N'tom@ya.com')
INSERT INTO [dbo].[Person] ([Id], [Login], [Password], [Email]) VALUES (4, N'qwe', N'qwe', N'qwerty@ya.com')
INSERT INTO [dbo].[Person] ([Id], [Login], [Password], [Email]) VALUES (5, N'zxc', N'zxc', N'zxc@ya.com')
SET IDENTITY_INSERT [dbo].[Person] OFF

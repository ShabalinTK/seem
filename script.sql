CREATE TABLE [dbo].[Comments] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (100)  NOT NULL,
    [Email]       NVARCHAR (100)  NULL,
    [CommentText] NVARCHAR (1000) NOT NULL,
    [MovieId]     INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comments_Post] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id])
);

CREATE TABLE [dbo].[Directors] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (255) NOT NULL,
    [Country] NVARCHAR (100) NOT NULL,
    [Age]     INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

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

CREATE TABLE [dbo].[MovieStats] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [MovieId] INT NOT NULL,
    [UserId]  INT NOT NULL,
    [IsLike]  BIT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MovieStats_Movie] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id]),
    CONSTRAINT [FK_MovieStats_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Person] ([Id])
);

CREATE TABLE [dbo].[Person] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Login]    NVARCHAR (50)  NOT NULL,
    [Password] NVARCHAR (50)  NOT NULL,
    [Email]    NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
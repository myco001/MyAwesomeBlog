CREATE TABLE [dbo].[Posts] (
    [PostId]      INT IDENTITY (1, 1) NOT NULL,
    [UserId]      NVARCHAR (MAX) NOT NULL,
    [PostHeading] NVARCHAR (MAX) NOT NULL,
    [PostContent] NVARCHAR (MAX) NOT NULL,
    [PostDate]    DATETIME NOT NULL,
    PRIMARY KEY (PostId)
);
GO



CREATE TABLE [dbo].[Comments] (
    [CommentId]      INT IDENTITY (1, 1) NOT NULL,
    [UserId]         NVARCHAR (MAX) NULL,
    [CommentContent] NVARCHAR (MAX) NOT NULL,
    [CommentDate]    DATETIME NOT NULL,
    [PostId]         INT NOT NULL,
    PRIMARY KEY (CommentId),
    FOREIGN KEY (PostId) REFERENCES Posts(PostId)
);
GO
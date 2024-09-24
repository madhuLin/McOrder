CREATE TABLE [dbo].[Category] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Type]       INT           NOT NULL,
    [Sort]       INT           DEFAULT ((0)) NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [State]      INT           DEFAULT ((1)) NOT NULL,
    [CreateTime] DATETIME2 (0) DEFAULT (sysdatetime()) NULL,
    [UpdateTime] DATETIME2 (0) DEFAULT (sysdatetime()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


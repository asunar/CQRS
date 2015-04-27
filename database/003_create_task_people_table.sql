CREATE TABLE [dbo].[Tasks_People](
	[TaskId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Tasks_People]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_People_People] FOREIGN KEY([PersonId])
REFERENCES [dbo].[People] ([PersonId])
GO

ALTER TABLE [dbo].[Tasks_People] CHECK CONSTRAINT [FK_Tasks_People_People]
GO

ALTER TABLE [dbo].[Tasks_People]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_People_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([TaskId])
GO

ALTER TABLE [dbo].[Tasks_People] CHECK CONSTRAINT [FK_Tasks_People_Tasks]
GO


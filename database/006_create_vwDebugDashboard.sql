CREATE VIEW [dbo].[vw_DebugDashboard]
AS
SELECT dbo.People.PersonId, dbo.People.Name, dbo.Tasks.Name AS Expr1, dbo.Tasks.DueDate, dbo.Tasks.Instructions, dbo.Tasks.CompletionDate, 
               dbo.Tasks.CompletionComment, dbo.Tasks.Status
FROM  dbo.People INNER JOIN
               dbo.Tasks_People ON dbo.People.PersonId = dbo.Tasks_People.PersonId INNER JOIN
               dbo.Tasks ON dbo.Tasks_People.TaskId = dbo.Tasks.TaskId

GO
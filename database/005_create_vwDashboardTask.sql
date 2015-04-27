CREATE VIEW [dbo].[vw_DashboardTask]
AS
SELECT dbo.Tasks.TaskId, dbo.Tasks.Name, dbo.Tasks.DueDate, dbo.Tasks.Status, dbo.Tasks_People.PersonId
FROM  dbo.Tasks INNER JOIN
               dbo.Tasks_People ON dbo.Tasks.TaskId = dbo.Tasks_People.TaskId

GO
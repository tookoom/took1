/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 100 [AppLogID]
      ,[LogTimestamp]
      ,[Level]
      ,[Message]
      ,[Data]
  FROM [tk1].[TK1].[AppLog]
  ORDER BY [AppLogID] DESC
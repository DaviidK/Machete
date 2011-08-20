/****** Script for SelectTopNRows command from SSMS  ******/

delete from macheteConnection.dbo.Employers
delete from macheteConnection.dbo.WorkOrders
delete from macheteConnection.dbo.WorkAssignments
delete from macheteConnection.dbo.WorkerRequests
delete from macheteConnection.dbo.workersignins
delete from macheteConnection.dbo.workers
delete from macheteConnection.dbo.Persons
delete from macheteConnection.dbo.images
go
set IDENTITY_INSERT macheteConnection.dbo.Employers ON
insert into macheteConnection.dbo.Employers ([ID],[active],[name],[address1],[address2],[city],[state],[phone],[cellphone],[zipcode],[email],[referredby],[referredbyOther],[blogparticipate],[datecreated],[dateupdated],[Createdby],[Updatedby])
SELECT [ID],[active],[name],[address1],[address2],[city],[state],[phone],[cellphone],[zipcode],[email],[referredby],[referredbyOther],'0',[datecreated],[dateupdated],[Createdby],[Updatedby]
  FROM [macheteStageProd].[dbo].[Employers]
go
set IDENTITY_INSERT macheteConnection.dbo.Employers OFF
go
/**************************************************/
set IDENTITY_INSERT macheteConnection.dbo.WorkOrders ON
insert macheteConnection.[dbo].[WorkOrders] ([ID],[EmployerID],[paperOrderNum],[contactName],[status],[workSiteAddress1],[workSiteAddress2],[city],[state],[phone],[zipcode],[typeOfWorkID],[englishRequired],[englishRequiredNote],[lunchSupplied],[permanentPlacement],[transportMethodID],[transportFee],[transportFeeExtra],[description],[dateTimeofWork],[timeFlexible],[datecreated],[dateupdated],[Createdby],[Updatedby], [waPseudoIDCounter])
SELECT [ID],[EmployerID],[paperOrderNum],[contactName],[status],[workSiteAddress1],[workSiteAddress2],[city],[state],[phone],[zipcode],[typeOfWorkID],[englishRequired],[englishRequiredNote],[lunchSupplied],[permanentPlacement],[transportMethodID],[transportFee],[transportFeeExtra],[description],[dateTimeofWork],[timeFlexible],[datecreated],[dateupdated],[Createdby],[Updatedby],[waPseudoIDCounter]
  FROM [macheteStageProd].[dbo].[WorkOrders]
GO
set IDENTITY_INSERT macheteConnection.dbo.WorkOrders OFF
GO
/**************************************************/
set IDENTITY_INSERT macheteConnection.dbo.WorkAssignments ON
insert macheteConnection.[dbo].[WorkAssignments] ([ID],[workerAssignedID],[workOrderID],[workerSigninID],[active],[pseudoID],[description],[englishLevelID],[skillID],[hourlyWage],[hours],[days],[qualityOfWork],[followDirections],[attitude],[reliability],[transportProgram],[comments],[datecreated],[dateupdated],[Createdby],[Updatedby],[WorkerID])
SELECT [ID],[workerAssignedID],[workOrderID],[workerSigninID],[active],[pseudoID],[description],[englishLevelID],[skillID],[hourlyWage],[hours],[days],[qualityOfWork],[followDirections],[attitude],[reliability],[transportProgram],[comments],[datecreated],[dateupdated],[Createdby],[Updatedby],[WorkerID]
  FROM [macheteStageProd].[dbo].[WorkAssignments]
GO
set IDENTITY_INSERT macheteConnection.dbo.WorkAssignments OFF
go
/**************************************************/
set IDENTITY_INSERT macheteConnection.dbo.Persons ON
insert macheteConnection.dbo.persons ([ID],[active],[firstname1],[firstname2],[lastname1],[lastname2],[address1],[address2],[city],[state],[zipcode],[phone],[gender],[genderother],[datecreated],[dateupdated],[Createdby],[Updatedby])
SELECT [ID],[active],[firstname1],[firstname2],[lastname1],[lastname2],[address1],[address2],[city],[state],[zipcode],[phone],[gender],[genderother],[datecreated],[dateupdated],[Createdby],[Updatedby]
  FROM [macheteStageProd].[dbo].[Persons]
GO
set IDENTITY_INSERT macheteConnection.dbo.Persons OFF
go
/**************************************************/
insert macheteConnection.dbo.workers ([ID],[typeOfWorkID],[dateOfMembership],[dateOfBirth],[active],[RaceID],[raceother],[height],[weight],[englishlevelID],[recentarrival],[dateinUSA],[dateinseattle],[disabled],[disabilitydesc],[maritalstatus],[livewithchildren],[numofchildren],[incomeID],[livealone],[emcontUSAname],[emcontUSArelation],[emcontUSAphone],[dwccardnum],[neighborhoodID],[immigrantrefugee],[countryoforiginID],[emcontoriginname],[emcontoriginrelation],[emcontoriginphone],[memberexpirationdate],[driverslicense],[licenseexpirationdate],[carinsurance],[insuranceexpiration],[ImageID],[skill1],[skill2],[skill3],[datecreated],[dateupdated],[Createdby],[Updatedby])
SELECT w.[ID],case p.gender when 38 then 20 when 39 then 21 end,[dateOfMembership],[dateOfBirth],w.[active],[RaceID],[raceother],[height],[weight],[englishlevelID],[recentarrival],[dateinUSA],[dateinseattle],[disabled],[disabilitydesc],[maritalstatus],[livewithchildren],[numofchildren],[incomeID],[livealone],[emcontUSAname],[emcontUSArelation],[emcontUSAphone],[dwccardnum],[neighborhoodID],[immigrantrefugee],[countryoforiginID],[emcontoriginname],[emcontoriginrelation],[emcontoriginphone],[memberexpirationdate],[driverslicense],[licenseexpirationdate],[carinsurance],[insuranceexpiration],[ImageID],[skill1],[skill2],[skill3],w.[datecreated],w.[dateupdated],w.[Createdby],w.[Updatedby]
  FROM [macheteStageProd].[dbo].[Workers] w join machetestageprod.dbo.persons p on p.ID = w.ID
GO
/**************************************************/
set IDENTITY_INSERT macheteConnection.dbo.WorkerSignins ON
insert macheteConnection.dbo.workersignins ([ID],[dwccardnum],[WorkerID],[WorkAssignmentID],[dateforsignin],[datecreated],[dateupdated],[Createdby],[Updatedby])
SELECT [ID],[dwccardnum],[WorkerID],[WorkAssignmentID],[dateforsignin],[datecreated],[dateupdated],[Createdby],[Updatedby]
  FROM [macheteStageProd].[dbo].[WorkerSignins]
GO
set IDENTITY_INSERT macheteConnection.dbo.WorkerSignins OFF
go
/**************************************************/
set IDENTITY_INSERT macheteConnection.dbo.Images ON
insert macheteConnection.dbo.images ([ID],[ImageData],[ImageMimeType],[filename],[Thumbnail],[ThumbnailMimeType],[parenttable],[recordkey],[datecreated],[dateupdated],[Createdby],[Updatedby])
SELECT [ID],[ImageData],[ImageMimeType],[filename],[Thumbnail],[ThumbnailMimeType],[parenttable],[recordkey],[datecreated],[dateupdated],[Createdby],[Updatedby]
  FROM [machetestageprod].[dbo].[Images]
GO
set IDENTITY_INSERT macheteConnection.dbo.Images OFF
go
/**************************************************/
set IDENTITY_INSERT macheteConnection.dbo.workerrequests ON
insert macheteConnection.dbo.workerrequests ([ID],[WorkOrderID],[WorkerID],[datecreated],[dateupdated],[Createdby],[Updatedby])
SELECT [ID],[WorkOrderID],[WorkerID],[datecreated],[dateupdated],[Createdby],[Updatedby]
  FROM [macheteStageProd].[dbo].[WorkerRequests]
GO
set IDENTITY_INSERT macheteConnection.dbo.workerrequests OFF
go
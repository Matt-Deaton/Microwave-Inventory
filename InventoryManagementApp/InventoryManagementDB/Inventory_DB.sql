/* Check whether the database exists and delete if so. */
IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'Inventory_DB')
BEGIN
	DROP DATABASE [Inventory_DB] 
	PRINT '' PRINT '*** Dropping Inventory_DB'
END
GO

PRINT '' PRINT '*** Creating Inventory_DB'
GO
CREATE DATABASE [Inventory_DB]
GO

PRINT '' PRINT '*** Using Inventory_DB'
GO
USE [Inventory_DB]
GO

						/* CREATING TABLES */
PRINT ''
PRINT '******************************************************'
PRINT '*******************Creating Tables********************'

PRINT '' PRINT '*** Creating Employee Table'
GO

CREATE TABLE [dbo].[Employee] (
	[EmployeeID]		[int] IDENTITY(1000000,1) 	NOT NULL,
	[FirstName]			[nvarchar](50) 				NOT NULL,
	[LastName]			[nvarchar](50) 				NOT NULL,
	[Email]				[nvarchar](250) 			NOT NULL,
	[PhoneNumber]		[nvarchar](11) 				NOT NULL,
	[PasswordHash]		[nvarchar](100)				NOT NULL DEFAULT
	'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]			[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_Email]	UNIQUE([Email] ASC)
)
GO

PRINT '' PRINT '*** Adding Index for LastName on Employee Table'
GO
CREATE NONCLUSTERED INDEX [ix_lastName]
	ON[Employee]([LastName] ASC)
GO

PRINT '' PRINT '*** Creating Role Table'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]			[nvarchar](50)				NOT NULL,
	[Description]		[nvarchar](250) 			NULL,
	CONSTRAINT	[pk_RoleID]	PRIMARY KEY([RoleID] ASC)
)
GO

PRINT '' PRINT '*** Creating Employee Role Table'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeID]		[int] 					 	NOT NULL,
	[RoleID]			[nvarchar](50)				NOT NULL,
	CONSTRAINT	[pk_EmployeeID_RoleID]	
		PRIMARY KEY([EmployeeID] ASC, [RoleID] ASC),
	CONSTRAINT	[fk_employeeRole_employeeID] FOREIGN KEY([EmployeeID])
		REFERENCES[Employee]([EmployeeID]) ON UPDATE CASCADE,
	CONSTRAINT	[fk_employeeRole_roleID] FOREIGN KEY([RoleID])
		REFERENCES[Role]([RoleID]) ON UPDATE CASCADE
)
GO

PRINT '' PRINT '*** Creating Line Table'
GO
CREATE TABLE [dbo].[Line] (
	[LineID]			[nvarchar](5)			NOT NULL,
	[Description]		[nvarchar](500)			NOT NULL,
	CONSTRAINT [pk_LineID]	PRIMARY KEY([LineID] ASC)
)
GO

PRINT '' PRINT '*** Creating Model Table'
GO
CREATE TABLE [dbo].[Model](
	[ModelNumber]		[nvarchar](10)			NOT NULL,
	[LineID]			[nvarchar](5)			NOT NULL,
	[Watts]				[int]					NOT NULL,
	[Description]		[nvarchar](250),
	CONSTRAINT [pk_ModelNumber]	PRIMARY KEY([ModelNumber] ASC),
	CONSTRAINT [fk_Line_LineID] FOREIGN KEY([LineID])
		REFERENCES[Line]([LineID])
)
GO

PRINT '' PRINT '*** Creating Part Table'
GO
CREATE TABLE [dbo].[Part] (
	[PartNumber]		[nvarchar](10)				NOT NULL,
	[PartName]			[nvarchar](30)				NOT NULL,
	[Cost]				[money]						NULL,
	[Description]		[nvarchar](200)				NULL,
	[Active]			[bit]						NOT NULL DEFAULT 1
	CONSTRAINT	[pk_PartNumber] PRIMARY KEY([PartNumber] ASC)
)
GO

PRINT '' PRINT '*** Creating Location Table'
GO
CREATE TABLE [dbo].[Location] (
	[LocationNumber]	[nvarchar](4)				NOT NULL,
	CONSTRAINT	[pk_LocationNumber] PRIMARY KEY([LocationNumber]ASC)
)
GO

PRINT '' PRINT '*** Creating Part Location Table'
GO
CREATE TABLE [dbo].[PartLocation] (
	[PartNumber]		[nvarchar](10)				NOT NULL,
	[LocationNumber]	[nvarchar](4)				NOT NULL,
	[EmployeeID]		[int]						NULL,
	[Quantity]			[int]						NOT NULL,
	CONSTRAINT	[pk_PartNumber_LocationNumber]
		PRIMARY KEY([PartNumber] ASC, [LocationNumber] ASC),
	CONSTRAINT	[fk_partLocation_partNumber] FOREIGN KEY([PartNumber])
		REFERENCES[Part]([PartNumber]),
	CONSTRAINT	[fk_partLocation_LocationNumber] FOREIGN KEY([LocationNumber])
		REFERENCES[Location]([LocationNumber]),
	CONSTRAINT	[fk_partLocation_EmployeeID] FOREIGN KEY([EmployeeID])
		REFERENCES[Employee]([EmployeeID])
)
GO

					/* CREATE SAMPLE DATA */
PRINT ''
PRINT '******************************************************'
PRINT '*****************Creating Sample Data*****************'

PRINT '' PRINT '*** Creating Sample Line Records'
GO
INSERT INTO [dbo].[Line]
	([LineID], [Description])
	VALUES
	('HDC','Microwave built in Cedar Rapids, Iowa built on Line 1.'),
	('RC','Microwave built in Cedar Rapids, Iowa built on Line 2.'),
	('RMS','Microwave built in Cedar Rapids, Iowa built on Line 3.'),
	('RCS','Microwave built in Cedar Rapids, Iowa built on Line 4.'),
	('JET','Convection oven built in Cedar Rapids, Iowa built on Line 5.'),
	('MXP','Convection oven built in Cedar Rapids, Iowa built on Line 6.'),
	('MRX','Convection oven built in Cedar Rapids, Iowa built on Line 7.'),
	('ONCUE','Microwave built in Cedar Rapids, Iowa built on Line 8, when MSO is not running.'),
	('MSO','Microwave Steamer built in Cedar Rapids, Iowa built on Line 8 when ONCUE is not running.'),
	('RFS','Microwave built in Cedar Rapids, Iowa built on Line 9.')
GO

PRINT '' PRINT '*** Creating Sample Employee Records'
GO
INSERT INTO [dbo].[Employee]
	([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
	('Matt', 'Deaton', '16418313134','mattyd45@mwb.com'),
	('Michael', 'Scott', '13085555555','greatscott@mwb.com'),
	('Dwight', 'Schrute', '13088265962','beetfarmer97@mwb.com'),
	('Darryl','Philbin','13087368686','dbphilbin23@mwb.com'),
	('Nate', 'Nickerson', '13081234567', 'naters@mwb.com')
GO

PRINT '' PRINT '*** Creating Sample Role Records'
GO
INSERT INTO [dbo].[Role]
	([RoleID])
	VALUES
	('Administrator'),
	('Manager'),
	('Material Handler'),
	('Recieving Clerk'),
	('Buyer')
GO

PRINT '' PRINT '*** Inserting Sample Employee Role Records'
GO
INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID], [RoleID])
	VALUES
	(1000000, 'Administrator'),
	(1000001, 'Manager'),
	(1000002, 'Buyer'),
	(1000003, 'Recieving Clerk'),
	(1000004, 'Material Handler')
GO

PRINT '' PRINT '*** Creating Sample Parts Records'
GO
INSERT INTO [dbo].[Part]
	([PartNumber],[PartName],[Cost],[Description])
	VALUES
	('10489421','Mag Tube', 5.10, 'Mag tube for HDC Unit'),
	('12486615','Basepan', 3.20, 'Painted basepan for HDC Unit'),
	('12133605','Strirer Motor', 1.25, 'Bottom Stirrer Motor for HDC Unit'),
	('12133606','Stirrer Motor', 1.25, 'Top Stirrer Motor for HDC Unit'),
	('20171234','Cavity', 15.10, 'Cavity from Weld Department'),
	('12990529','Screw', .01, 'Screw, 1/4 inch'),
	('12991234','Antenna', .61, 'Pemmed part, Antenna for HDC'),
	('20039205','Transformer', 11.25, 'Transformer for Domestic 120V HDC Unit'),
	('20173501','Wiring Harness', 4.25, 'Wiring Harnes for Domestic 120V HDC Unit'),
	('12730004','Capacitor', 1.25, 'Capacitor, Multiply Lines use')
GO

PRINT '' PRINT '*** Creating Sample Locations Records'
GO
INSERT INTO [dbo].[Location]
			([LocationNumber])
	VALUES
	('201A'),('201B'),('201C'),('201D'),('201E'),
	('202A'),('202B'),('202C'),('202D'),('202E'),
	('203A'),('203B'),('203C'),('203D'),('203E'),
	('204A'),('204B'),('204C'),('204D'),('204E'),
	('205A'),('205B'),('205C'),('205D'),('205E'),
	('206A'),('206B'),('206C'),('206D'),('206E'),
	('207A'),('207B'),('207C'),('207D'),('207E'),
	('208A'),('208B'),('208C'),('208D'),('208E'),
	('209A'),('209B'),('209C'),('209D'),('209E'),
	('210A'),('210B'),('210C'),('210D'),('210E'),
	('301A'),('301B'),('301C'),('301D'),('301E'),
	('302A'),('302B'),('302C'),('302D'),('302E'),
	('303A'),('303B'),('303C'),('303D'),('303E'),
	('304A'),('304B'),('304C'),('304D'),('304E'),
	('305A'),('305B'),('305C'),('305D'),('305E'),
	('306A'),('306B'),('306C'),('306D'),('306E'),
	('307A'),('307B'),('307C'),('307D'),('307E'),
	('308A'),('308B'),('308C'),('308D'),('308E'),
	('309A'),('309B'),('309C'),('309D'),('309E'),
	('310A'),('310B'),('310C'),('310D'),('310E'),
	('401A'),('401B'),('401C'),('401D'),('401E'),
	('402A'),('402B'),('402C'),('402D'),('402E'),
	('403A'),('403B'),('403C'),('403D'),('403E'),
	('404A'),('404B'),('404C'),('404D'),('404E'),
	('405A'),('405B'),('405C'),('405D'),('405E'),
	('406A'),('406B'),('406C'),('406D'),('406E'),
	('407A'),('407B'),('407C'),('407D'),('407E'),
	('408A'),('408B'),('408C'),('408D'),('408E'),
	('409A'),('409B'),('409C'),('409D'),('409E'),
	('410A'),('410B'),('410C'),('410D'),('410E'),
	('501A'),('501B'),('501C'),('501D'),('501E'),
	('502A'),('502B'),('502C'),('502D'),('502E'),
	('503A'),('503B'),('503C'),('503D'),('503E'),
	('504A'),('504B'),('504C'),('504D'),('504E'),
	('505A'),('505B'),('505C'),('505D'),('505E'),
	('506A'),('506B'),('506C'),('506D'),('506E'),
	('507A'),('507B'),('507C'),('507D'),('507E'),
	('508A'),('508B'),('508C'),('508D'),('508E'),
	('509A'),('509B'),('509C'),('509D'),('509E'),
	('510A'),('510B'),('510C'),('510D'),('510E'),
	('001A'),('WELD'),('CAGE')
GO

PRINT '' PRINT '*** Creating Sample Part Location Records'
GO
INSERT INTO [dbo].[PartLocation]
	([PartNumber],[LocationNumber],[Quantity])
	VALUES
	('10489421','201A', 540),
	('12486615','210B', 80),
	('12133605','405D', 600),
	('12133606','302E', 480),
	('20171234','WELD', 84),
	('12990529','CAGE', 10000),
	('12991234','303C', 250),
	('20039205','503A', 72),
	('20173501','507B', 90),
	('12486615','308D', 50)	
GO

					/* CREATE STORED PROCEDURES */
PRINT ''
PRINT '******************************************************'
PRINT '**************Creating Stored Procedures**************'

PRINT '' PRINT '*** Creating sp_insert_employee'
GO
CREATE PROCEDURE [sp_insert_employee]
(
	@FirstName		[nvarchar](50),
	@LastName		[nvarchar](50),
	@PhoneNumber	[nvarchar](11),
	@Email			[nvarchar](250)
)
AS
BEGIN
	INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email])
	VALUES
		(@FirstName, @LastName, @PhoneNumber, LOWER(@Email))
	SELECT SCOPE_IDENTITY()
END
GO

PRINT '' PRINT'*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [sp_authenticate_user]
(
	@Email			[nvarchar](250),
	@PasswordHash	[nvarchar](100)
)
AS
BEGIN
	SELECT 	COUNT([EmployeeID])
	FROM	[dbo].[Employee]
	WHERE	[Email] = LOWER(@Email)
	AND		[PasswordHash] = @PasswordHash
	AND		[Active] = 1
END
GO

PRINT '' PRINT '*** Creating sp_update_email'
GO
CREATE PROCEDURE [sp_update_email]
(
	@OldEmail			[nvarchar](250),
	@NewEmail			[nvarchar](250),
	@PasswordHash		[nvarchar](100)
)
AS
BEGIN
	UPDATE	[dbo].[Employee]
	SET		[Email] = LOWER(@NewEmail)
	WHERE	[Email] = LOWER(@OldEmail)
	AND		[PasswordHash] = @PasswordHash
	AND		[Active] = 1
	RETURN	@@ROWCOUNT
END
GO

PRINT '' PRINT '*** Creating sp_select_user_by_email'
GO
CREATE PROCEDURE [sp_select_user_by_email]
(
	@Email		[nvarchar](250)
)
AS
BEGIN
	SELECT [EmployeeID],[FirstName],[LastName],[PhoneNumber]
	FROM [Employee]
	WHERE [Email] = @Email
END
GO

PRINT '' PRINT '*** Creating sp_select_roles_by_userID'
GO
CREATE PROCEDURE [sp_select_roles_by_userID]
(
	@EmployeeID		[int]
)
AS
BEGIN
	SELECT [RoleID]
	FROM [EmployeeRole]
	WHERE [EmployeeID] = @EmployeeID
END
GO
PRINT '' PRINT '*** Creating sp_update_password'
GO
CREATE PROCEDURE [sp_update_password]
(
	@EmployeeID			[int],
	@OldPasswordHash 	[nvarchar](100),
	@NewPasswordHash 	[nvarchar](100)
)
AS
BEGIN
	UPDATE	[dbo].[Employee]
	SET		[PasswordHash] = @NewPasswordHash
	WHERE	[EmployeeID] = @EmployeeID
	AND		[PasswordHash] = @OldPasswordHash
	AND		[Active] = 1
	RETURN	@@ROWCOUNT
END
GO

PRINT '' PRINT '*** Creating sp_select_users_by_active'
GO
CREATE PROCEDURE [sp_select_users_by_active]
(
	@Active			[bit]
)
AS
BEGIN
	SELECT 		[EmployeeID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
	FROM 		[dbo].[Employee]
	WHERE 		[Active] = @Active
	ORDER BY 	[LastName]
END
GO

PRINT '' PRINT '*** Creating sp_select_employee_by_id'
GO
CREATE PROCEDURE [sp_select_employee_by_id]
(
	@EmployeeID		[int]
)
AS
BEGIN
	SELECT 		[EmployeeID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
	FROM 		[dbo].[Employee]
	WHERE 		[EmployeeID] = @EmployeeID
END
GO

PRINT '' PRINT '*** Creating sp_update_employee'
GO
CREATE PROCEDURE [sp_update_employee]
(
	@EmployeeID			[int],

	@NewFirstName		[nvarchar](50),
	@NewLastName		[nvarchar](50),
	@NewPhoneNumber 	[nvarchar](11),
	@NewEmail 			[nvarchar](250),
	
	@OldFirstName		[nvarchar](50),
	@OldLastName		[nvarchar](50),
	@OldPhoneNumber 	[nvarchar](11),
	@OldEmail 			[nvarchar](250)
)
AS
BEGIN
	UPDATE [dbo].[Employee]
		SET [FirstName] = 	@NewFirstName,
			[LastName] = 	@NewLastName,
			[PhoneNumber] = @NewPhoneNumber,
			[Email] = 		@NewEmail
	WHERE 	[EmployeeID] =	@EmployeeID  
	  AND	[FirstName] = 	@OldFirstName
	  AND	[LastName] = 	@OldLastName
	  AND	[PhoneNumber] = @OldPhoneNumber
	  AND	[Email] = 		@OldEmail
	 
	RETURN @@ROWCOUNT
END
GO

PRINT '' PRINT '*** Creating sp_deactivate_employee'
GO
CREATE PROCEDURE [sp_deactivate_employee]
(
	@EmployeeID			[int]
)
AS
BEGIN
	UPDATE [dbo].[Employee]
		SET [Active] = 		0
	WHERE 	[EmployeeID] =	@EmployeeID  
	RETURN @@ROWCOUNT
END
GO

PRINT '' PRINT '*** Creating sp_reactivate_employee'
GO
CREATE PROCEDURE [sp_reactivate_employee]
(
	@EmployeeID			[int]
)
AS
BEGIN
	UPDATE [dbo].[Employee]
		SET [Active] = 		1
	WHERE 	[EmployeeID] =	@EmployeeID  
	RETURN @@ROWCOUNT
END
GO

PRINT '' PRINT '*** Creating sp_insert_employee_role'
GO
CREATE PROCEDURE [sp_insert_employee_role]
(
	@EmployeeID			[int],
	@RoleID				[nvarchar](50)
)
AS
BEGIN
INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID], [RoleID])
	VALUES
	(@EmployeeID, @RoleID)
END
GO

PRINT '' PRINT '*** Creating sp_delete_employee_role'
GO
CREATE PROCEDURE [sp_delete_employee_role]
(
	@EmployeeID			[int],
	@RoleID				[nvarchar](50)
)
AS
BEGIN
	DELETE FROM [dbo].[EmployeeRole]
	WHERE [EmployeeID] =	@EmployeeID
	  AND [RoleID] = 		@RoleID
END
GO

PRINT '' PRINT '*** Creating sp_select_all_roles'
GO
CREATE PROCEDURE [sp_select_all_roles]
AS
BEGIN
	SELECT [RoleID]
	FROM [dbo].[Role]
	ORDER BY [RoleID]
END
GO

PRINT '' PRINT '*** Creating sp_select_by_part_number'
GO
CREATE PROCEDURE [sp_select_by_part_number]
(
	@PartNumber		[nvarchar](10)
)
AS
BEGIN
	SELECT	[PartLocation].[PartNumber]
			, [PartLocation].[Quantity]
			, [Part].[PartName]
			, [Location].[LocationNumber]
			, [Part].[Description]
			, [Part].[Cost]
	FROM [PartLocation] 
	INNER JOIN [Part] 
	ON	[PartLocation].[PartNumber] 
			= [Part].[PartNumber]
			
	INNER JOIN [Location] 
	ON	[PartLocation].[LocationNumber]
			= [Location].[LocationNumber]
	WHERE [PartLocation].[PartNumber] = @PartNumber
END
GO
	
PRINT '' PRINT '*** Creating sp_recieve_part'
GO
CREATE PROCEDURE [sp_recieve_part]
(
	@PartNumber				[nvarchar](10),
	@Quantity				[int],
	@LocationNumber			[nvarchar](4)
)
AS
BEGIN
	INSERT INTO [dbo].[PartLocation]
		([PartNumber], [Quantity], [LocationNumber])
	VALUES
		(@PartNumber, @Quantity, @LocationNumber)
	SELECT SCOPE_IDENTITY()
END
GO

PRINT '' PRINT '*** Creating sp_select_parts'
GO
CREATE PROCEDURE[sp_select_parts]
AS
BEGIN
	SELECT	[PartLocation].[PartNumber]
			, [PartLocation].[Quantity]
			, [Part].[PartName]
			, [Location].[LocationNumber]
	FROM [PartLocation] 
	INNER JOIN [Part] 
	ON	[PartLocation].[PartNumber] 
			= [Part].[PartNumber]
			
	INNER JOIN [Location] 
	ON	[PartLocation].[LocationNumber]
			= [Location].[LocationNumber]
	ORDER BY[PartNumber], [Quantity]
END
GO

PRINT '' PRINT '*** Creating sp_insert_new_part'
GO
CREATE PROCEDURE [sp_insert_new_part]
(
	@PartNumber				[nvarchar](10),
	@PartName				[nvarchar](30),
	@Cost					[money],
	@Description			[nvarchar](200)
)
AS
BEGIN
	INSERT INTO [dbo].[Part]
		([PartNumber], [PartName], [Cost], [Description])
	VALUES
		(@PartNumber, @PartName, @Cost, @Description)
	SELECT SCOPE_IDENTITY()
END 
GO

PRINT '' PRINT '*** Creating sp_update_amount'
GO
CREATE PROCEDURE [sp_update_amount] 
(
	@PartNumber				[nvarchar](10),
	@Amount					[int],
	@LocationNumber 		[nvarchar](4)
)
AS	
BEGIN
	UPDATE	[dbo].[PartLocation]
	SET		[Quantity] = [Quantity] - @Amount
	WHERE	[PartNumber] = @PartNumber
	AND		[LocationNumber] = @LocationNumber
	RETURN	@@ROWCOUNT
END
GO


PRINT '' PRINT '*** Creating sp_move_parts'
GO
CREATE PROCEDURE [sp_move_parts]
(
	@PartNumber				[nvarchar](10),
	@OldQuantity			[int],
	@OldLocationNumber		[nvarchar](4),
	@NewQuantity			[int],
	@NewLocationNumber		[nvarchar](4)
)
AS
BEGIN
	UPDATE	[dbo].[PartLocation]
	SET		[Quantity] = @NewQuantity,
			[LocationNumber] = @NewLocationNumber
	WHERE	[PartNumber] = @PartNumber
	AND		[Quantity] = @OldQuantity
	AND		[LocationNumber] = @OldLocationNumber
	RETURN	@@ROWCOUNT
END
GO

PRINT '' PRINT '*** Creating sp_select_all_part_information'
GO
CREATE PROCEDURE [sp_select_all_part_information]
AS
BEGIN
	SELECT	[PartNumber],
			[PartName],
			[Cost],
			[Description]
	FROM 	[dbo].[Part]
	ORDER BY[PartNumber]
END
GO

PRINT '' PRINT '*** Creating sp_update_part_informtion'
GO
CREATE PROCEDURE [sp_update_part_informtion]
(
	@PartNumber			[nvarchar](10),
	
	@NewPartName		[nvarchar](30),
	@NewCost			[money],
	@NewDescription		[nvarchar](200),
	
	@OldPartName		[nvarchar](30),
    @OldCost			[money],
    @OldDescription		[nvarchar](200)
)
AS
BEGIN
	UPDATE [dbo].[Part]
		SET	[PartName] = @NewPartName,
			[Cost] = @NewCost,
			[Description] = @NewDescription
	WHERE	[PartNumber] = @PartNumber
	AND		[PartName] = @OldPartName
	AND		[Cost] = @OldCost
	AND		[Description] = @OldDescription
	
	RETURN @@ROWCOUNT
END
GO
	
PRINT '' PRINT '*** Creating sp_deactivate_part'
GO
CREATE PROCEDURE [sp_deactivate_part]
(
	@PartNumber			[nvarchar](10)
)
AS
BEGIN
	UPDATE [dbo].[Part]
	SET [Active] = 0
	WHERE [PartNumber] = @PartNumber
	RETURN @@ROWCOUNT
END
GO
	
PRINT '' PRINT '*** Creating sp_activate_part'
GO
CREATE PROCEDURE [sp_activate_part]
(
	@PartNumber			[nvarchar](10)
)
AS
BEGIN
	UPDATE [dbo].[Part]
	SET [Active] = 1
	WHERE [PartNumber] = @PartNumber
	RETURN @@ROWCOUNT
END
GO

PRINT '' PRINT '*** Creating sp_select_part_information_by_part_number'
GO
CREATE PROCEDURE [sp_select_part_information_by_part_number]
(
	@PartNumber		[nvarchar](10)
)
AS
BEGIN
	SELECT	[PartNumber]
			,[PartName]
			,[Description]
			,[Cost]
	FROM [Part]
	WHERE [PartNumber] = @PartNumber
END
GO
	
PRINT '' PRINT '*** Creating sp_delete_part_by_part_number'
GO
CREATE PROCEDURE [sp_delete_part_by_part_number]
(
	@PartNumber			[nvarchar](10)
)
AS
BEGIN
	DELETE
	FROM [dbo].[Part]
	WHERE [PartNumber] = @PartNumber

	RETURN @@ROWCOUNT
END
GO

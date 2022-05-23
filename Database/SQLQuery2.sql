create database PenZu
go

use PenZu
go


create table UserAccount(
	UserID char(5),
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null,
	EmailAddress nvarchar(100),
	UserName nvarchar(100) not null,
	Pass nvarchar(100) not null,
	
	CONSTRAINT PK_User PRIMARY KEY (UserID)
);
GO

create table EntryTable(
	EntryID char(5),
	NameEntry nvarchar(500),
	Content nvarchar(4000),
	Star bit,
	Emotion nvarchar(50),
	DateOfEntry date,
	DateUpload date,
	UserID char(5) not null FOREIGN KEY REFERENCES UserAccount(UserID),
	TextFont nvarchar(100),
	FontStyle nvarchar(100),
	SizeOfText float,
	CONSTRAINT PK_Entry PRIMARY KEY (EntryID)
);
GO

create table Folder(
	FolderID char(5),
	FolderName nvarchar(200),
	UserID char(5) not null FOREIGN KEY REFERENCES UserAccount(UserID),
	CONSTRAINT PK_Folder PRIMARY KEY (FolderID)
);
go

create table Img(
	ImageID char(5),
	ImagePath nchar(300) not null,
	FolderID char(5) not null FOREIGN KEY REFERENCES Folder(FolderID),
	EntryID char(5) not null FOREIGN KEY REFERENCES EntryTable(EntryID),
	UserID char(5) not null FOREIGN KEY REFERENCES UserAccount(UserID)
	CONSTRAINT PK_Img PRIMARY KEY (ImageID)
);
go


insert into UserAccount
values 
	('00000','Khoa','Duong','dvkhoa19@apcs.fitus.edu.vn','dvkhoa19','voicanui'),
	('00001','Tuan','Vo','vntuan19@apcs.fitus.edu.vn','vntuan19','khongcocrush'),
	('00002','Tri','Cao','cttri19@apcs.fitus.edu.vn','cttri19','crushn10nam');

insert into EntryTable
values
	('00000','Happy day','<div id=\"diary\" contenteditable=\"true\" role=\"textbox\" style=\"background-color: whitesmoke; \" data-placeholder=\"Note what you want in here ...\">Today is a great day.</div>',0 ,Null,'2022-05-11','2022-05-11','00000','Times New Roman','Bold',12),
	('00001','Sad day','<div id=\"diary\" contenteditable=\"true\" role=\"textbox\" style=\"background-color: whitesmoke; \" data-placeholder=\"Note what you want in here ...\">Today is a desperate day.</div>',0 ,Null,'2022-05-12','2022-05-12','00000','Times New Roman','Bold',12),
	('00002','Love day','<div id=\"diary\" contenteditable=\"true\" role=\"textbox\" style=\"background-color: whitesmoke; \" data-placeholder=\"Note what you want in here ...\">I just want to sleep.</div>',0 ,Null,'2022-05-11','2022-05-11','00000','Times New Roman','Bold',12);

insert into Folder
values
	('00000','Beach','00000'),
	('00001','Mountain','00000'),
	('00002','Forest','00000');

insert into Img
values
	('00000','Image12224717477.jpg','00000','00000','00000'),
	('00001','Image12224717478.jpg','00001','00000','00000'),
	('00002','Image12224717479.jpg','00002','00000','00000');


GO

-----------------------------------------------------------------------------



---Search Entry-----------------------------------------------------------------------------
CREATE OR ALTER FUNCTION GetEntryByTime(@userid char(5), @startdate date, @enddate date)
RETURNS TABLE
AS
RETURN
(
    select e.NameEntry, e.DateOfEntry, e.Emotion, e.Star
	from EntryTable as e
	where e.UserID =@userid and @startdate <= e.DateOfEntry and e.DateOfEntry<= @enddate
)
GO

CREATE OR ALTER FUNCTION GetEntryByEmotion(@userid char(5), @Emotion nvarchar(50))
RETURNS TABLE
AS
RETURN
(
    select e.NameEntry, e.DateOfEntry, e.Emotion, e.Star
	from EntryTable as e
	where e.UserID =@userid and e.Emotion=@Emotion
)
GO

CREATE OR ALTER FUNCTION GetEntryByStar(@userid char(5), @Star bit)
RETURNS TABLE
AS
RETURN
(
    select e.NameEntry, e.DateOfEntry, e.Emotion, e.Star
	from EntryTable as e
	where e.UserID =@userid and e.Star=@Star
)
GO

CREATE OR ALTER FUNCTION GetEntryByTimeEmotion(@userid char(5), @startdate date, @enddate date, @Emotion nvarchar(50))
RETURNS TABLE
AS
RETURN
(
    select e.NameEntry, e.DateOfEntry, e.Emotion, e.Star
	from EntryTable as e
	where e.UserID =@userid and @startdate <= e.DateOfEntry and e.DateOfEntry<= @enddate and e.Emotion=@Emotion
)
GO

---Search Image-----------------------------------------------------------------------------
CREATE OR ALTER FUNCTION GetImageByTime(@userid char(5), @startdate date, @enddate date)
RETURNS TABLE
AS
RETURN
(
    select i.EntryID, i.FolderID, i.ImagePath
	from Img as i join EntryTable as e on i.EntryID=e.EntryID
	where i.UserID =@userid and @startdate <= e.DateOfEntry and e.DateOfEntry<= @enddate
)
GO

CREATE OR ALTER FUNCTION getImageByFolder(@userID char(5), @FolderName nvarchar(200))
RETURNS TABLE
AS
RETURN
(
    select i.EntryID, i.FolderID, i.ImagePath
	from Img as i join Folder as f on i.FolderID=f.FolderID
	where i.UserID=@userID and f.FolderName=@FolderName
)
GO

CREATE OR ALTER FUNCTION getImageByEmotion(@userID char(5), @Emotion nvarchar(50))
RETURNS TABLE
AS
RETURN
(
    select i.EntryID, i.FolderID, i.ImagePath
	from Img as i join EntryTable as e on i.EntryID=e.EntryID
	where e.UserID =@userid and e.Emotion=@Emotion
)
GO



-----------------------------------------------------------------------------


/*
select* from GetEntryByPeriod('00000','2021-01-01','2023-01-01')
*/

CREATE OR ALTER FUNCTION GetFirstEntryOfUser(@userid char(5))
RETURNS TABLE
AS
RETURN
(
    select top(1) e.EntryID, e.NameEntry, e.DateOfEntry, e.Emotion, e.Star
	from EntryTable as e
	where e.UserID =@userid 
	order by e.DateOfEntry desc
)
GO

/*
select* from GetFirstEntryOfUser('00000')
*/

CREATE OR ALTER FUNCTION GetEntryInforByID(@entryID char(5))
RETURNS TABLE
AS
RETURN
(
    select *
	from EntryTable as e
	where e.EntryID =@entryID
)
GO

/*
select* from GetEntryInforByID('00000')
*/

CREATE OR ALTER FUNCTION GetListEntryByUserID(@userid char(5))
RETURNS TABLE
AS
RETURN
(
    select top(100) *
	from EntryTable as e
	where e.UserID =@userid
	order by e.DateOfEntry desc
)
GO

/*
select* from GetListEntryByUserID('00000')
*/

CREATE OR ALTER PROCEDURE sp_NewEntry(@nameEntry nvarchar(500), @content nvarchar(4000), @star bit, @emotion nvarchar(50), @dateofentry date, @dateupload date, @userid char(5), @textfont nvarchar(100), @fontstyle nvarchar(100),@sizeoftext float, @result INT OUT)
AS
BEGIN TRANSACTION
        if exists(
            select *
            from UserAccount
            where UserID = @userid
        )
        BEGIN
			Declare @count int
			Set @count=(
				select top(1) e.EntryID
				from EntryTable as e
				order by e.EntryID desc
			)
			Set @count=CAST(@count AS int)
			Set @count=@count+1
			Declare @tmp char(5)
			Set @tmp=RIGHT('00000'+CAST(@count as varchar(5)) ,5)
			insert into EntryTable(EntryID, NameEntry ,Content , Star, Emotion, DateOfEntry, DateUpload, UserID, TextFont, FontStyle, SizeOfText)
			values (@tmp, @nameEntry, N''+@content+'', @star,@emotion,@dateofentry,@dateupload,@userid, @textfont, @fontstyle, @sizeoftext);
			Set @result=1

        END
		ELSE
		BEGIN
			Set @result=0
		END
       
COMMIT TRANSACTION
    
GO

--------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_DeleteEntry(@entryID char(5), @result INT OUT)
AS
BEGIN TRANSACTION
        if exists(
            select *
            from EntryTable
            where EntryID = @entryID
        )
        BEGIN
			delete from Img where EntryID = @entryID
			DELETE FROM EntryTable WHERE EntryID = @entryID 
			Set @result=1

        END
		ELSE
		BEGIN
			Set @result=0
		END
       
COMMIT TRANSACTION
    
GO
----------------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE sp_SaveEntry(@userid char(5), @entryid char(5), @nameEntry nvarchar(500), @content nvarchar(4000), @star bit, @emotion nvarchar(50), @dateofentry date, @dateupload date, @textfont nvarchar(100), @fontstyle nvarchar(100),@sizeoftext float, @result INT OUT)
AS
BEGIN TRANSACTION
        if exists(
            select *
            from EntryTable
            where EntryID = @entryID
        )
        BEGIN
			Update EntryTable
			set NameEntry=@nameEntry, Content=N''+@content+'', Star=@star, Emotion=@emotion, DateOfEntry=@dateofentry, TextFont=@textfont, FontStyle=@fontstyle, SizeOfText=@sizeoftext
			where EntryID=@entryid
		Set @result=1

        END
		ELSE
		BEGIN
			Set @result=0
		END
       
COMMIT TRANSACTION
    
GO

---------------------------------------------------------------------------------------------------------------------------------------


CREATE OR ALTER FUNCTION GetListImageByFolderName(@userid char(5), @foldername nvarchar(200))
RETURNS TABLE
AS
RETURN
(
    select i.ImageID, i.EntryID, i.FolderID, i.ImagePath, i.UserID, f.FolderName
	from Img as i join Folder as f on i.FolderID=f.FolderID
	where f.FolderName=@foldername and i.UserID=@userid
)
GO

---------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER FUNCTION GetListImageByEntryID(@userid char(5), @entryid char(5))
RETURNS TABLE
AS
RETURN
(
    select i.ImageID, i.EntryID, i.FolderID, i.ImagePath, i.UserID
	from Img as i 
	where i.UserID=@userid and i.EntryID=@entryid
)
GO
---------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE sp_NewImage(@UserID nvarchar(100), @FolderName nvarchar(100), @EntryID char(5), @ImagePath char(100), @result INT OUT)
AS
BEGIN TRANSACTION
			Declare @count int
			Set @count=(
				select top(1) i.ImageID
				from Img as i
				order by i.ImageID desc
			)
			Set @count=CAST(@count AS int)
			Set @count=@count+1
			Declare @tmp char(5)
			Set @tmp=RIGHT('00000'+CAST(@count as varchar(5)) ,5)

			Declare @FolderID char(5)
			Set @FolderID=(
				select top(1) f.FolderID
				from Folder as f
				where f.UserID=@UserID and f.FolderName=@FolderName
			)

			insert into Img(ImageID, ImagePath ,FolderID , EntryID, UserID)
			values (@tmp, @ImagePath, @FolderID, @EntryID,@UserID);
			Set @result=1

       
COMMIT TRANSACTION
GO

---------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE sp_DeleteImage(@imageID char(5), @result INT OUT)
AS
BEGIN TRANSACTION
        if exists(
            select *
            from Img
            where ImageID = @imageID
        )
        BEGIN
			DELETE FROM Img WHERE ImageID = @imageID
			Set @result=1

        END
		ELSE
		BEGIN
			Set @result=0
		END
       
COMMIT TRANSACTION
    
GO

---------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_createAccountUser(@firstname nvarchar(200), @lastname nvarchar(200), @username nchar(30), @pass nchar(30), @email nchar(100),@result INT OUT)
AS
BEGIN TRANSACTION
		set @result=2
        if not exists(
            select *
            from UserAccount
            where UserName = @username
        )
        BEGIN
			Declare @count int
			Set @count=(
				select top(1) u.UserID
				from UserAccount as u
				order by u.UserID desc
			)
			Set @count=CAST(@count AS int)
			Set @count=@count+1
			Declare @tmp char(5)
			Set @tmp=RIGHT('00000'+CAST(@count as varchar(5)) ,5)
			insert into UserAccount(UserID, FirstName ,LastName , EmailAddress, UserName, Pass)
			values (@tmp, @firstname, @lastname, @email,@username,@pass);
			Set @result=1
			
        END
		ELSE
		BEGIN
			Set @result=0
		END
       
COMMIT TRANSACTION
    
GO

---------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER FUNCTION GetUserID(@userName nvarchar(100))
RETURNS TABLE
AS
RETURN
(
    select u.UserID
	from UserAccount as u
	where u.UserName=@userName
)
GO

---------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_CheckSignIn(@username nchar(30), @pass nchar(30), @result INT OUT)
AS
BEGIN TRANSACTION
		set @result=2
        if not exists(
            select *
            from UserAccount
            where UserName = @username
        )
        BEGIN
            Set @result=0
        END
		ELSE
		BEGIN
			if not exists(
            select *
            from UserAccount
            where Pass = @pass)
			BEGIN
				Set @result=1
			END
		END
       
COMMIT TRANSACTION
    
GO
/*
declare @result INT
exec sp_CheckSignIn 'cttri19','crushn10nam', @result OUT
print(@result)
*/

---------------------------------------------------------------------------------------------------------------------------------------
---Search image theo filter gi?
---------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER FUNCTION getAllImageFolderByUserID(@userID char(5))
RETURNS TABLE
AS
RETURN
(
    select f.FolderID, f.FolderName, count(i.ImageID) as NumberOfImage
	from Folder as f join Img as i on f.FolderID=i.FolderID
	where f.UserID=@userID
	group by f.FolderID, f.FolderName
)
GO

/*
select * from getAllImageFolderByUserID('00000')
*/

CREATE OR ALTER FUNCTION getRepresentativeImagePathOfFolder(@FolderID char(5))
RETURNS TABLE
AS
RETURN
(
    select top(1) i.ImagePath
	from Folder as f join Img as i on f.FolderID=i.FolderID
	where f.FolderID=@FolderID
)
GO

/*
select * from getRepresentativeImagePathOfFolder('00001')
*/

---------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER FUNCTION getAllImageByUserID(@userID char(5))
RETURNS TABLE
AS
RETURN
(
    select *
	from Img as i
	where i.UserID=@userID
)
GO
---------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_createFolder(@userID char(5), @folderName nvarchar(200), @result INT OUT)
AS
BEGIN TRANSACTION
		set @result=2
        if not exists(
            select *
            from Folder as f 
            where f.FolderName = @folderName and f.UserID=@userID
        )
        BEGIN
			Declare @count int
			Set @count=(
				select top(1) f.FolderID
				from Folder as f
				order by f.FolderID desc
			)
			Set @count=CAST(@count AS int)
			Set @count=@count+1
			Declare @tmp char(5)
			Set @tmp=RIGHT('00000'+CAST(@count as varchar(5)) ,5)
			insert into Folder(FolderID, FolderName ,UserID)
			values (@tmp, @folderName, @userID);
			Set @result=1
			
        END
		ELSE
		BEGIN
			Set @result=0
		END
       
COMMIT TRANSACTION
    
GO


CREATE OR ALTER FUNCTION GetFirstImageOfFolder(@userID char(5), @FolderName nvarchar(200))
RETURNS TABLE
AS
RETURN
(
    select top(1) i.EntryID, i.FolderID, i.ImagePath
	from Img as i join Folder as f on i.FolderID=f.FolderID
	where i.UserID=@userID and f.FolderName=@FolderName
)
GO

CREATE OR ALTER FUNCTION GetListFolderNameByUserID(@UserID char(5))
RETURNS TABLE
AS
RETURN
(
    select top(100) FolderName
	from Folder as f
	where f.UserID =@UserID
)
GO
/*
use master
drop database PenZu
*/
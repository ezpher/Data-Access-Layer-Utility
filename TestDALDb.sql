USE [TestDAL]
GO
/****** Object:  Table [dbo].[TestDALTable]    Script Date: 11/4/2021 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestDALTable](
	[BigInt] [bigint] NULL,
	[NVarchar(50)] [nvarchar](50) NULL,
	[Varchar(50)] [varchar](50) NULL,
	[DateTime2] [datetime2](7) NULL,
	[Decimal] [decimal](7, 2) NULL,
	[Bit] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_ClearTestDALTable]    Script Date: 11/4/2021 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ClearTestDALTable]
AS
BEGIN

	DELETE FROM TestDAL..TestDALTable

END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertIntoTestDALTable]    Script Date: 11/4/2021 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertIntoTestDALTable]
	@BigInt BIGINT,
	@NVarChar50 NVARCHAR(50),
	@VarChar50 VARCHAR(50),
	@DateTime2 DATETIME2(7),
	@Decimal DECIMAL(7,2),
	@Bit BIT
AS
BEGIN
	
	-- Make sure default values are different from what is supplied in program when inserting
	DECLARE @BigIntb BIGINT
	SET @BigIntb = COALESCE(@BigInt, 1)

	DECLARE @NVarChar50b NVARCHAR(50)
	SET @NVarChar50b = COALESCE(@NVarChar50, N'Anything')

	DECLARE @VarChar50b VARCHAR(50)
	SET @VarChar50b = COALESCE(@VarChar50, 'Anything')

	DECLARE @DateTime2b DATETIME2(7)
	SET @DateTime2b = COALESCE(@DateTime2, '19000101')

	DECLARE @Decimalb DECIMAL(7,2)
	SET @Decimalb = COALESCE(@Decimal, 123.12)

	DECLARE @Bitb BIT
	SET @Bitb = COALESCE(@Bit, 0)

	INSERT INTO TestDAL..TestDALTable (BigInt, [NVarchar(50)], [Varchar(50)], DateTime2, Decimal, Bit) VALUES (@BigIntb, @NVarChar50b, @VarChar50b, @DateTime2b, @Decimalb, @Bitb)

END
GO
/****** Object:  StoredProcedure [dbo].[SP_QueryTestDALTable]    Script Date: 11/4/2021 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_QueryTestDALTable]
AS
BEGIN

	SELECT 
		BigInt, 
		[NVarchar(50)], 
		[Varchar(50)], 
		DateTime2, 
		Decimal, 
		Bit 
	FROM TestDAL..TestDALTable

END
GO
/****** Object:  StoredProcedure [dbo].[SP_QueryTestDALTableWithParams]    Script Date: 11/4/2021 2:17:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_QueryTestDALTableWithParams]
	@BigInt BIGINT,
	@NVarChar50 NVARCHAR(50),
	@VarChar50 VARCHAR(50),
	@DateTime2 DATETIME2(7),
	@Decimal DECIMAL(7,2),
	@Bit BIT
AS
BEGIN
	
	-- Make sure default values are different from what is supplied in program
	DECLARE @BigIntb BIGINT
	SET @BigIntb = COALESCE(@BigInt, 1)

	DECLARE @NVarChar50b NVARCHAR(50)
	SET @NVarChar50b = COALESCE(@NVarChar50, N'Anything')

	DECLARE @VarChar50b VARCHAR(50)
	SET @VarChar50b = COALESCE(@VarChar50, 'Anything')

	DECLARE @DateTime2b DATETIME2(7)
	SET @DateTime2b = COALESCE(@DateTime2, '19000101')

	DECLARE @Decimalb DECIMAL(7,2)
	SET @Decimalb = COALESCE(@Decimal, 123.12)

	DECLARE @Bitb BIT
	SET @Bitb = COALESCE(@Bit, 0)

	SELECT 
		BigInt, 
		[NVarchar(50)], 
		[Varchar(50)], 
		DateTime2, 
		Decimal, 
		Bit 
	FROM TestDAL..TestDALTable
	WHERE 
		BigInt = @BigIntb 
		OR [NVarchar(50)] = @NVarChar50b 
		OR [Varchar(50)] = @VarChar50b 
		OR DateTime2 = @DateTime2b 
		OR Decimal = @Decimalb 
		OR Bit = @Bitb
END
GO

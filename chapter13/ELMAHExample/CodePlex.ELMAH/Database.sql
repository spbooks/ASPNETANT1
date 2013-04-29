/*

Author(s):

    Atif Aziz (atif.aziz@skybow.com, http://www.raboof.com)

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

CREATE TABLE dbo.ELMAH_Error
(
	ErrorId     UNIQUEIDENTIFIER NOT NULL,
	Application NVARCHAR(60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Host        NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Type        NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Source      NVARCHAR(60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Message     NVARCHAR(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[User]      NVARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	StatusCode  INT NOT NULL,
	TimeUtc     DATETIME NOT NULL,
	Sequence    INT IDENTITY (1, 1) NOT NULL,
	AllXml      NTEXT COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE dbo.ELMAH_Error WITH NOCHECK ADD 
	CONSTRAINT PK_ELMAH_Error PRIMARY KEY  CLUSTERED 
	(
		ErrorId
	)  ON [PRIMARY] 
GO

ALTER TABLE dbo.ELMAH_Error ADD 
	CONSTRAINT DF_ELMAH_Error_ErrorId DEFAULT (newid()) FOR [ErrorId]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.ELMAH_GetErrorXml
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

SET NOCOUNT ON

SELECT 
    AllXml
FROM 
    ELMAH_Error
WHERE
    ErrorId = @ErrorId
AND
    Application = @Application



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.ELMAH_GetErrorsXml
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

SET NOCOUNT ON

DECLARE @Page TABLE
(
    Position INT IDENTITY(1, 1) NOT NULL,
    ErrorId UNIQUEIDENTIFIER NOT NULL,
    Application NVARCHAR(60) NOT NULL,
    Host NVARCHAR(30) NOT NULL,
    Type NVARCHAR(100) NOT NULL,
    Source NVARCHAR(60) NOT NULL,
    Message NVARCHAR(500) NOT NULL,
    [User] NVARCHAR(50) NOT NULL,
    StatusCode INT NOT NULL,
    TimeUtc DATETIME NOT NULL
)

INSERT
INTO
    @Page
    (
        ErrorId,
        Application,
        Host,
        Type,
        Source,
        Message,
        [User],
        StatusCode,
        TimeUtc
    )
SELECT
    ErrorId,
    Application,
    Host,
    Type,
    Source,
    Message,
    [User],
    StatusCode,
    TimeUtc
FROM
    ELMAH_Error
WHERE
    Application = @Application    
ORDER BY
    TimeUtc DESC,
    Sequence DESC

SELECT 
    @TotalCount = COUNT(*) 
FROM 
    @Page

DECLARE @FirstPosition INT
SET @FirstPosition = @PageIndex * @PageSize + 1

DECLARE @LastPosition INT
SET @LastPosition  = @FirstPosition + @PageSize - 1

SELECT 
    errorId, 
    application,
    host, 
    type,
    source,
    message,
    [user],
    statusCode, 
    CONVERT(VARCHAR(50), TimeUtc, 126) + 'Z' time
FROM 
    @Page error
WHERE
    Position >= @FirstPosition
AND
    Position <= @LastPosition
ORDER BY
    Position
FOR
    XML AUTO

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.ELMAH_LogError
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

SET NOCOUNT ON

INSERT
INTO
    ELMAH_Error
    (
        ErrorId,
        Application,
        Host,
        Type,
        Source,
        Message,
        [User],
        AllXml,
        StatusCode,
        TimeUtc
    )
VALUES
    (
        @ErrorId,
        @Application,
        @Host,
        @Type,
        @Source,
        @Message,
        @User,
        @AllXml,
        @StatusCode,
        @TimeUtc
    )

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


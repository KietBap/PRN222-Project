USE [FpturoomBooking]
GO
/****** Object:  Table [dbo].[BookingRoomSlots]    Script Date: 3/26/2025 7:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingRoomSlots](
	[BookingId] [int] NOT NULL,
	[RoomSlotId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC,
	[RoomSlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 3/26/2025 7:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](50) NOT NULL,
	[Purpose] [nvarchar](500) NULL,
	[BookingStatus] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[BookingDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Campus]    Script Date: 3/26/2025 7:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campus](
	[CampusId] [int] IDENTITY(1,1) NOT NULL,
	[CampusName] [nvarchar](255) NOT NULL,
	[Location] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[CampusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 3/26/2025 7:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 3/26/2025 7:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [nvarchar](255) NOT NULL,
	[Capacity] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CampusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomSlots]    Script Date: 3/26/2025 7:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomSlots](
	[RoomSlotId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[SlotNumber] [int] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomSlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/26/2025 7:50:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserCode] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[CampusId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (4, 5)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (4, 6)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (4, 7)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (4, 8)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (5, 9)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (5, 10)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (5, 11)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (5, 12)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (6, 5)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (6, 6)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (6, 7)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (6, 11)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (6, 12)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (7, 8)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (7, 9)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (7, 10)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (8, 5)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (9, 5)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (10, 5)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (11, 21)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (11, 22)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (11, 23)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (11, 24)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (11, 25)
INSERT [dbo].[BookingRoomSlots] ([BookingId], [RoomSlotId]) VALUES (12, 26)
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (1, N'U001', N'Họp nhóm', N'Pending', CAST(N'2025-03-26T15:34:56.753' AS DateTime), NULL)
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (2, N'U002', N'Trình bày đồ án', N'Approved', CAST(N'2025-03-26T15:34:56.753' AS DateTime), NULL)
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (3, N'U003', N'Tổ chức hội thảo', N'Rejected', CAST(N'2025-03-26T15:34:56.753' AS DateTime), NULL)
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (4, N'U004', N'ok', N'Cancelled', CAST(N'2025-03-26T16:05:29.347' AS DateTime), CAST(N'2025-03-27' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (5, N'U002', N'ggg', N'Pending', CAST(N'2025-03-26T16:07:13.007' AS DateTime), CAST(N'2025-03-27' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (6, N'U011', N'dasdas', N'Cancelled', CAST(N'2025-03-26T16:08:20.167' AS DateTime), CAST(N'2025-03-26' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (7, N'U004', N'2222', N'Cancelled', CAST(N'2025-03-26T16:08:33.920' AS DateTime), CAST(N'2025-03-26' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (8, N'U010', N'r', N'Cancelled', CAST(N'2025-03-26T16:21:42.450' AS DateTime), CAST(N'2025-03-27' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (9, N'U010', N'er', N'Pending', CAST(N'2025-03-26T16:35:16.510' AS DateTime), CAST(N'2025-03-26' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (10, N'U010', N'xc', N'Pending', CAST(N'2025-03-26T16:51:33.150' AS DateTime), CAST(N'2025-03-27' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (11, N'U011', N'2', N'Pending', CAST(N'2025-03-26T19:16:58.587' AS DateTime), CAST(N'2025-03-27' AS Date))
INSERT [dbo].[Bookings] ([BookingId], [UserCode], [Purpose], [BookingStatus], [CreatedDate], [BookingDate]) VALUES (12, N'U011', N're', N'Pending', CAST(N'2025-03-26T19:17:20.573' AS DateTime), CAST(N'2025-03-27' AS Date))
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Campus] ON 

INSERT [dbo].[Campus] ([CampusId], [CampusName], [Location]) VALUES (1, N'FPT Hà Nội', N'Khu Công nghệ cao Hòa Lạc, Hà Nội')
INSERT [dbo].[Campus] ([CampusId], [CampusName], [Location]) VALUES (2, N'FPT Đà Nẵng', N'Khu đô thị FPT City, Ngũ Hành Sơn, Đà Nẵng')
INSERT [dbo].[Campus] ([CampusId], [CampusName], [Location]) VALUES (3, N'FPT TP.HCM', N'Trường FPT, Quận 9, TP.HCM')
INSERT [dbo].[Campus] ([CampusId], [CampusName], [Location]) VALUES (4, N'FPT Cần Thơ', N'Đại lộ Hòa Bình, Cần Thơ')
SET IDENTITY_INSERT [dbo].[Campus] OFF
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (1, N'Công ngh? thông tin')
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (2, N'Kinh doanh')
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (3, N'Thi?t k? d? h?a')
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (4, N'Ngôn ng? Anh')
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (5, N'Qu?n tr? khách s?n')
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (1, N'Phòng Lab 101', 30, N'Available', 1)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (2, N'Phòng Hội thảo 202', 50, N'Available', 2)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (3, N'Phòng Học 303', 40, N'Available', 3)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (4, N'Phòng Thực hành 404', 25, N'Available', 4)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (5, N'Phòng Lab 101', 30, N'Available', 1)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (6, N'Phòng Lab 102', 35, N'Available', 1)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (7, N'Phòng Học 201', 40, N'Available', 2)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (8, N'Phòng Học 202', 45, N'Available', 2)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (9, N'Phòng Hội thảo 301', 50, N'Available', 3)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (10, N'Phòng Hội thảo 302', 55, N'Available', 3)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (11, N'Phòng Thực hành 401', 25, N'Available', 4)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (12, N'Phòng Thực hành 402', 30, N'Available', 4)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (13, N'Phòng Họp 501', 20, N'Available', 1)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (14, N'Phòng Họp 502', 22, N'Available', 1)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (15, N'Phòng Lab 103', 33, N'Available', 2)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (16, N'Phòng Lab 104', 38, N'Available', 2)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (17, N'Phòng Học 203', 42, N'Available', 3)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (18, N'Phòng Học 204', 46, N'Available', 3)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (19, N'Phòng Hội thảo 303', 52, N'Available', 4)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (20, N'Phòng Hội thảo 304', 58, N'Available', 4)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (21, N'Phòng Thực hành 403', 28, N'Available', 1)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (22, N'Phòng Thực hành 404', 32, N'Available', 2)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (23, N'Phòng Họp 503', 24, N'Available', 3)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [Capacity], [Status], [CampusId]) VALUES (24, N'Phòng Họp 504', 26, N'Available', 4)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomSlots] ON 

INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (5, 1, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (6, 1, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (7, 1, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (8, 1, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (9, 1, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (10, 1, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (11, 1, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (12, 1, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (13, 2, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (14, 2, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (15, 2, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (16, 2, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (17, 2, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (18, 2, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (19, 2, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (20, 2, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (21, 3, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (22, 3, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (23, 3, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (24, 3, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (25, 3, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (26, 3, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (27, 3, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (28, 3, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (29, 4, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (30, 4, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (31, 4, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (32, 4, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (33, 4, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (34, 4, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (35, 4, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (36, 4, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (37, 5, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (38, 5, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (39, 5, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (40, 5, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (41, 5, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (42, 5, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (43, 5, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (44, 5, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (45, 6, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (46, 6, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (47, 6, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (48, 6, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (49, 6, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (50, 6, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (51, 6, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (52, 6, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (53, 7, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (54, 7, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (55, 7, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (56, 7, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (57, 7, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (58, 7, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (59, 7, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (60, 7, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (61, 8, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (62, 8, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (63, 8, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (64, 8, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (65, 8, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (66, 8, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (67, 8, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (68, 8, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (69, 9, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (70, 9, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (71, 9, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (72, 9, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (73, 9, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (74, 9, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (75, 9, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (76, 9, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (77, 10, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (78, 10, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (79, 10, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (80, 10, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (81, 10, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (82, 10, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (83, 10, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (84, 10, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (85, 11, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (86, 11, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (87, 11, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (88, 11, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (89, 11, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (90, 11, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (91, 11, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (92, 11, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (93, 12, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (94, 12, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (95, 12, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (96, 12, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (97, 12, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (98, 12, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (99, 12, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (100, 12, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (101, 13, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (102, 13, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (103, 13, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
GO
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (104, 13, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (105, 13, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (106, 13, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (107, 13, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (108, 13, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (109, 14, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (110, 14, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (111, 14, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (112, 14, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (113, 14, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (114, 14, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (115, 14, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (116, 14, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (117, 15, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (118, 15, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (119, 15, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (120, 15, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (121, 15, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (122, 15, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (123, 15, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (124, 15, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (125, 16, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (126, 16, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (127, 16, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (128, 16, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (129, 16, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (130, 16, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (131, 16, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (132, 16, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (133, 17, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (134, 17, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (135, 17, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (136, 17, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (137, 17, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (138, 17, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (139, 17, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (140, 17, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (141, 18, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (142, 18, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (143, 18, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (144, 18, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (145, 18, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (146, 18, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (147, 18, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (148, 18, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (149, 19, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (150, 19, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (151, 19, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (152, 19, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (153, 19, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (154, 19, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (155, 19, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (156, 19, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (157, 20, 1, CAST(N'00:00:00' AS Time), CAST(N'03:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (158, 20, 2, CAST(N'03:00:00' AS Time), CAST(N'06:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (159, 20, 3, CAST(N'06:00:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (160, 20, 4, CAST(N'09:00:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (161, 20, 5, CAST(N'12:00:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (162, 20, 6, CAST(N'15:00:00' AS Time), CAST(N'18:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (163, 20, 7, CAST(N'18:00:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[RoomSlots] ([RoomSlotId], [RoomId], [SlotNumber], [StartTime], [EndTime], [Status]) VALUES (164, 20, 8, CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time), N'Available')
SET IDENTITY_INSERT [dbo].[RoomSlots] OFF
GO
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U001', N'Nguyễn Văn A', N'nguyen@fpt.edu.vn', 1, 1, N'@1', N'Admin')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U002', N'Trần Thị B', N'tran@fpt.edu.vn', 2, 2, N'@1', N'User')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U003', N'Phạm Văn C', N'pham@fpt.edu.vn', 3, 3, N'@1', N'User')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U004', N'Lê Thị D', N'le@fpt.edu.vn', 4, 4, N'@1', N'Manager')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U005', N'Nguyễn Văn E', N'e@fpt.edu.vn', 1, 1, N'@1', N'Manager')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U006', N'Trần Thị F', N'f@fpt.edu.vn', 2, 2, N'@1', N'Manager')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U007', N'Phạm Văn G', N'g@fpt.edu.vn', 3, 3, N'@1', N'Manager')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U008', N'Lê Thị H', N'h@fpt.edu.vn', 4, 4, N'@1', N'Manager')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U009', N'Hoàng Minh I', N'i@fpt.edu.vn', 1, 2, N'@1', N'Manager')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U010', N'Bùi Văn J', N'j@fpt.edu.vn', 2, 3, N'@1', N'User')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U011', N'Đỗ Thị K', N'k@fpt.edu.vn', 3, 4, N'@1', N'User')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U012', N'Ngô Văn L', N'l@fpt.edu.vn', 4, 5, N'@1', N'User')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U013', N'Vũ Thị M', N'm@fpt.edu.vn', 1, 3, N'@1', N'User')
INSERT [dbo].[Users] ([UserCode], [FullName], [Email], [CampusId], [DepartmentId], [Password], [Role]) VALUES (N'U014', N'Phan Minh N', N'n@fpt.edu.vn', 2, 4, N'@1', N'User')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D105349C719756]    Script Date: 3/26/2025 7:50:28 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[BookingRoomSlots]  WITH CHECK ADD FOREIGN KEY([BookingId])
REFERENCES [dbo].[Bookings] ([BookingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingRoomSlots]  WITH CHECK ADD FOREIGN KEY([RoomSlotId])
REFERENCES [dbo].[RoomSlots] ([RoomSlotId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([UserCode])
REFERENCES [dbo].[Users] ([UserCode])
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD FOREIGN KEY([CampusId])
REFERENCES [dbo].[Campus] ([CampusId])
GO
ALTER TABLE [dbo].[RoomSlots]  WITH CHECK ADD FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([CampusId])
REFERENCES [dbo].[Campus] ([CampusId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
GO

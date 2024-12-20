USE [TechnoService]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[SecondName] [nvarchar](100) NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](75) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PositionID] [int] NOT NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[LastName] [nvarchar](70) NOT NULL,
	[SecondName] [nvarchar](70) NULL,
	[Phone] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipments]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SerialNumber] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](100) NULL,
	[TechSpec] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquipmentTypes]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FaultTypes]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FaultTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Priorities]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priorities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Priority] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestLogs]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RequestID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[Client] [int] NOT NULL,
	[Equipment] [int] NOT NULL,
	[FaultType] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Employee] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08.12.24 15:49:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](64) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ID], [FirstName], [LastName], [SecondName], [Phone], [Email]) VALUES (2, N'Алексей', N'Петров', NULL, N'89001234568', N'petrov@example.com')
INSERT [dbo].[Clients] ([ID], [FirstName], [LastName], [SecondName], [Phone], [Email]) VALUES (3, N'Мария', N'Сидорова', NULL, N'89001234569', N'sidorova@example.com')
INSERT [dbo].[Clients] ([ID], [FirstName], [LastName], [SecondName], [Phone], [Email]) VALUES (4, N'Ольга', N'Кузнецова', NULL, N'89001234570', N'kuznetsova@example.com')
INSERT [dbo].[Clients] ([ID], [FirstName], [LastName], [SecondName], [Phone], [Email]) VALUES (6, N'Петров', N'Петр', N'Петрович', N'+78621231212', N'petrovich@yandex.ru')
INSERT [dbo].[Clients] ([ID], [FirstName], [LastName], [SecondName], [Phone], [Email]) VALUES (7, N'Иванов', N'Иван', N'Иванович', N'+79299172902', N'divanov@gmail.com')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([ID], [UserID], [PositionID], [FirstName], [LastName], [SecondName], [Phone]) VALUES (1, 1, 1, N'Иван', N'Иванов', NULL, N'89001234567')
INSERT [dbo].[Employees] ([ID], [UserID], [PositionID], [FirstName], [LastName], [SecondName], [Phone]) VALUES (2, 2, 2, N'Алексей', N'Петров', NULL, N'89001234568')
INSERT [dbo].[Employees] ([ID], [UserID], [PositionID], [FirstName], [LastName], [SecondName], [Phone]) VALUES (3, 3, 3, N'Мария', N'Сидорова', NULL, N'89001234569')
INSERT [dbo].[Employees] ([ID], [UserID], [PositionID], [FirstName], [LastName], [SecondName], [Phone]) VALUES (4, 4, 4, N'Ольга', N'Кузнецова', NULL, N'89001234570')
INSERT [dbo].[Employees] ([ID], [UserID], [PositionID], [FirstName], [LastName], [SecondName], [Phone]) VALUES (6, 6, 1, N'Петров', N'Петр', N'Петрович', N'+76584321212')
INSERT [dbo].[Employees] ([ID], [UserID], [PositionID], [FirstName], [LastName], [SecondName], [Phone]) VALUES (7, 7, 4, N'Петров', N'Петр', N'Петрович', N'+79261577683')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Equipments] ON 

INSERT [dbo].[Equipments] ([ID], [Type], [Name], [SerialNumber], [Model], [TechSpec]) VALUES (2, 2, N'Принтер 1', N'SN002', N'Model P1', N'Черно-белый, лазерный')
INSERT [dbo].[Equipments] ([ID], [Type], [Name], [SerialNumber], [Model], [TechSpec]) VALUES (3, 3, N'Сканер 1', N'SN003', N'Model S1', N'1200dpi, цветной')
INSERT [dbo].[Equipments] ([ID], [Type], [Name], [SerialNumber], [Model], [TechSpec]) VALUES (4, 4, N'Маршрутизатор 5', N'SN005', N'Model R1', N'Wi-Fi, 5GHz')
SET IDENTITY_INSERT [dbo].[Equipments] OFF
GO
SET IDENTITY_INSERT [dbo].[EquipmentTypes] ON 

INSERT [dbo].[EquipmentTypes] ([ID], [Type]) VALUES (6, N'Клавиатура')
INSERT [dbo].[EquipmentTypes] ([ID], [Type]) VALUES (4, N'Маршрутизатор')
INSERT [dbo].[EquipmentTypes] ([ID], [Type]) VALUES (5, N'Монитор')
INSERT [dbo].[EquipmentTypes] ([ID], [Type]) VALUES (2, N'Принтер')
INSERT [dbo].[EquipmentTypes] ([ID], [Type]) VALUES (3, N'Сканер')
INSERT [dbo].[EquipmentTypes] ([ID], [Type]) VALUES (7, N'тест')
SET IDENTITY_INSERT [dbo].[EquipmentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[FaultTypes] ON 

INSERT [dbo].[FaultTypes] ([ID], [Type]) VALUES (1, N'Аппаратная ошибка')
INSERT [dbo].[FaultTypes] ([ID], [Type]) VALUES (5, N'Механическая ошибка')
INSERT [dbo].[FaultTypes] ([ID], [Type]) VALUES (4, N'Ошибка питания')
INSERT [dbo].[FaultTypes] ([ID], [Type]) VALUES (2, N'Программная ошибка')
INSERT [dbo].[FaultTypes] ([ID], [Type]) VALUES (3, N'Сетевая ошибка')
SET IDENTITY_INSERT [dbo].[FaultTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Positions] ON 

INSERT [dbo].[Positions] ([ID], [Position]) VALUES (3, N'Инженер')
INSERT [dbo].[Positions] ([ID], [Position]) VALUES (1, N'Менеджер')
INSERT [dbo].[Positions] ([ID], [Position]) VALUES (4, N'Менеджер по продажам')
INSERT [dbo].[Positions] ([ID], [Position]) VALUES (5, N'Сотрудник поддержки')
INSERT [dbo].[Positions] ([ID], [Position]) VALUES (2, N'Техник')
SET IDENTITY_INSERT [dbo].[Positions] OFF
GO
SET IDENTITY_INSERT [dbo].[Priorities] ON 

INSERT [dbo].[Priorities] ([ID], [Priority]) VALUES (3, N'Высокий')
INSERT [dbo].[Priorities] ([ID], [Priority]) VALUES (5, N'Критический')
INSERT [dbo].[Priorities] ([ID], [Priority]) VALUES (1, N'Низкий')
INSERT [dbo].[Priorities] ([ID], [Priority]) VALUES (2, N'Средний')
INSERT [dbo].[Priorities] ([ID], [Priority]) VALUES (4, N'Срочный')
SET IDENTITY_INSERT [dbo].[Priorities] OFF
GO
SET IDENTITY_INSERT [dbo].[RequestLogs] ON 

INSERT [dbo].[RequestLogs] ([ID], [LogDate], [Description], [RequestID]) VALUES (2, CAST(N'2024-12-02T00:00:00.000' AS DateTime), N'Запрос принят', 2)
INSERT [dbo].[RequestLogs] ([ID], [LogDate], [Description], [RequestID]) VALUES (3, CAST(N'2024-12-03T00:00:00.000' AS DateTime), N'Запрос принят', 3)
INSERT [dbo].[RequestLogs] ([ID], [LogDate], [Description], [RequestID]) VALUES (6, CAST(N'2024-12-06T07:26:46.487' AS DateTime), N'Создана новая заявка с ID 9.', 9)
SET IDENTITY_INSERT [dbo].[RequestLogs] OFF
GO
SET IDENTITY_INSERT [dbo].[Requests] ON 

INSERT [dbo].[Requests] ([ID], [RequestDate], [Client], [Equipment], [FaultType], [Priority], [Status], [Employee]) VALUES (2, CAST(N'2024-12-02T00:00:00.000' AS DateTime), 2, 2, 2, 2, 2, 3)
INSERT [dbo].[Requests] ([ID], [RequestDate], [Client], [Equipment], [FaultType], [Priority], [Status], [Employee]) VALUES (3, CAST(N'2024-12-03T00:00:00.000' AS DateTime), 3, 3, 3, 1, 3, 4)
INSERT [dbo].[Requests] ([ID], [RequestDate], [Client], [Equipment], [FaultType], [Priority], [Status], [Employee]) VALUES (8, CAST(N'2024-12-06T07:18:19.577' AS DateTime), 7, 3, 5, 3, 1, 3)
INSERT [dbo].[Requests] ([ID], [RequestDate], [Client], [Equipment], [FaultType], [Priority], [Status], [Employee]) VALUES (9, CAST(N'2024-12-06T07:26:46.323' AS DateTime), 4, 2, 5, 3, 1, 1)
SET IDENTITY_INSERT [dbo].[Requests] OFF
GO
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([ID], [Status]) VALUES (5, N'Архивировано')
INSERT [dbo].[Statuses] ([ID], [Status]) VALUES (1, N'В ожидании')
INSERT [dbo].[Statuses] ([ID], [Status]) VALUES (2, N'В процессе')
INSERT [dbo].[Statuses] ([ID], [Status]) VALUES (3, N'Завершено')
INSERT [dbo].[Statuses] ([ID], [Status]) VALUES (4, N'Отменено')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Login], [Password]) VALUES (1, N'ivanov', N'password1')
INSERT [dbo].[Users] ([ID], [Login], [Password]) VALUES (2, N'petrov', N'password2')
INSERT [dbo].[Users] ([ID], [Login], [Password]) VALUES (3, N'sidorova', N'password3')
INSERT [dbo].[Users] ([ID], [Login], [Password]) VALUES (4, N'kuznetsova', N'password4')
INSERT [dbo].[Users] ([ID], [Login], [Password]) VALUES (5, N'mikhailov', N'password5')
INSERT [dbo].[Users] ([ID], [Login], [Password]) VALUES (6, N'testuser1', N'E38AD214943DAAD1D64C102FAEC29DE4AFE9DA3D')
INSERT [dbo].[Users] ([ID], [Login], [Password]) VALUES (7, N'testuser2', N'2AA60A8FF7FCD473D321E0146AFD9E26DF395147')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Equipmen__048A00087C1FF1D1]    Script Date: 08.12.24 15:49:32 ******/
ALTER TABLE [dbo].[Equipments] ADD UNIQUE NONCLUSTERED 
(
	[SerialNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Equipmen__F9B8A48B11CD8A0B]    Script Date: 08.12.24 15:49:32 ******/
ALTER TABLE [dbo].[EquipmentTypes] ADD UNIQUE NONCLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__FaultTyp__F9B8A48BD3477207]    Script Date: 08.12.24 15:49:32 ******/
ALTER TABLE [dbo].[FaultTypes] ADD UNIQUE NONCLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Position__5A8B58B84D9A2B9B]    Script Date: 08.12.24 15:49:32 ******/
ALTER TABLE [dbo].[Positions] ADD UNIQUE NONCLUSTERED 
(
	[Position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Prioriti__534DF97BBED279A6]    Script Date: 08.12.24 15:49:32 ******/
ALTER TABLE [dbo].[Priorities] ADD UNIQUE NONCLUSTERED 
(
	[Priority] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Statuses__3A15923F4DBACE62]    Script Date: 08.12.24 15:49:32 ******/
ALTER TABLE [dbo].[Statuses] ADD UNIQUE NONCLUSTERED 
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RequestLogs] ADD  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[Requests] ADD  DEFAULT (getdate()) FOR [RequestDate]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([PositionID])
REFERENCES [dbo].[Positions] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Users]
GO
ALTER TABLE [dbo].[Equipments]  WITH CHECK ADD FOREIGN KEY([Type])
REFERENCES [dbo].[EquipmentTypes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequestLogs]  WITH CHECK ADD FOREIGN KEY([RequestID])
REFERENCES [dbo].[Requests] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD FOREIGN KEY([Client])
REFERENCES [dbo].[Clients] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD FOREIGN KEY([Employee])
REFERENCES [dbo].[Employees] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD FOREIGN KEY([Equipment])
REFERENCES [dbo].[Equipments] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD FOREIGN KEY([FaultType])
REFERENCES [dbo].[FaultTypes] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD FOREIGN KEY([Priority])
REFERENCES [dbo].[Priorities] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
ON DELETE CASCADE
GO

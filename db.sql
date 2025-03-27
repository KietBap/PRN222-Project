-- Tạo bảng Departments (Phòng ban)
CREATE TABLE Departments (
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(255) NOT NULL
);

-- Tạo bảng Campus (Cơ sở)
CREATE TABLE Campus (
    CampusId INT IDENTITY(1,1) PRIMARY KEY,
    CampusName NVARCHAR(255) NOT NULL,
    Location NVARCHAR(500) NULL
);



-- Tạo bảng Users (Người dùng)
CREATE TABLE Users (
    UserCode NVARCHAR(50) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    CampusId INT NOT NULL,
    DepartmentId INT NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CampusId) REFERENCES Campus(CampusId),
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId)
);

-- Tạo bảng Rooms (Phòng)
CREATE TABLE Rooms (
    RoomId INT IDENTITY(1,1) PRIMARY KEY,
    RoomName NVARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CampusId INT NOT NULL,
    FOREIGN KEY (CampusId) REFERENCES Campus(CampusId)
);

-- Tạo bảng RoomSlots (Khung giờ phòng)
CREATE TABLE RoomSlots (
    RoomSlotId INT IDENTITY(1,1) PRIMARY KEY,
    RoomId INT NOT NULL,
	SlotNumber INT NOT NULL,
    Date DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- Ví dụ: 'Available', 'Booked', 'Canceled'
    FOREIGN KEY (RoomId) REFERENCES Rooms(RoomId)
);

-- Tạo bảng Bookings (Đặt phòng)
CREATE TABLE Bookings (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    UserCode NVARCHAR(50) NOT NULL,
    Purpose NVARCHAR(500) NULL,
    BookingStatus NVARCHAR(50) NOT NULL, -- Ví dụ: 'Pending', 'Approved', 'Rejected'
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserCode) REFERENCES Users(UserCode)
);

-- Tạo bảng BookingRoomSlots (Liên kết đặt phòng & khung giờ)
CREATE TABLE BookingRoomSlots (
    BookingId INT NOT NULL,
    RoomSlotId INT NOT NULL,
    PRIMARY KEY (BookingId, RoomSlotId),
    FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId) ON DELETE CASCADE,
    FOREIGN KEY (RoomSlotId) REFERENCES RoomSlots(RoomSlotId) ON DELETE CASCADE
);

-- Chèn dữ liệu vào bảng Departments (Phòng ban)
INSERT INTO Departments (DepartmentName) VALUES
(N'Công nghệ thông tin'),
(N'Kinh doanh'),
(N'Thiết kế đồ họa'),
(N'Ngôn ngữ Anh'),
(N'Quản trị khách sạn');

-- Chèn dữ liệu vào bảng Campus (Cơ sở)
INSERT INTO Campus (CampusName, Location) VALUES
(N'FPT Hà Nội', N'Khu Công nghệ cao Hòa Lạc, Hà Nội'),
(N'FPT Đà Nẵng', N'Khu đô thị FPT City, Ngũ Hành Sơn, Đà Nẵng'),
(N'FPT TP.HCM', N'Trường FPT, Quận 9, TP.HCM'),
(N'FPT Cần Thơ', N'Đại lộ Hòa Bình, Cần Thơ');

-- Chèn dữ liệu vào bảng Users (Người dùng)
INSERT INTO Users (UserCode, FullName, Email, CampusId, DepartmentId, Password, Role) VALUES
('U001', N'Nguyễn Văn A', 'nguyen@fpt.edu.vn', 1, 1, '123456', 'Admin'),
('U002', N'Trần Thị B', 'tran@fpt.edu.vn', 2, 2, '123456', 'User'),
('U003', N'Phạm Văn C', 'pham@fpt.edu.vn', 3, 3, '123456', 'User'),
('U004', N'Lê Thị D', 'le@fpt.edu.vn', 4, 4, '123456', 'User');

-- Chèn dữ liệu vào bảng Rooms (Phòng)
INSERT INTO Rooms (RoomName, Capacity, Status, CampusId) VALUES
(N'Phòng Lab 101', 30, 'Available', 1),
(N'Phòng Hội thảo 202', 50, 'Available', 2),
(N'Phòng Học 303', 40, 'Available', 3),
(N'Phòng Thực hành 404', 25, 'Available', 4);

-- Chèn dữ liệu vào bảng RoomSlots (Khung giờ phòng)
INSERT INTO RoomSlots (RoomId, SlotNumber, Date, StartTime, EndTime, Status) VALUES
(1, 1, '2025-04-01', '08:00', '10:00', 'Available'),
(2, 2, '2025-04-01', '10:15', '12:15', 'Available'),
(3, 3, '2025-04-01', '13:30', '15:30', 'Available'),
(4, 4, '2025-04-01', '15:45', '17:45', 'Available');

-- Chèn dữ liệu vào bảng Bookings (Đặt phòng)
INSERT INTO Bookings (UserCode, Purpose, BookingStatus, CreatedDate) VALUES
('U001', N'Họp nhóm', 'Pending', GETDATE()),
('U002', N'Trình bày đồ án', 'Approved', GETDATE()),
('U003', N'Tổ chức hội thảo', 'Rejected', GETDATE());

-- Chèn dữ liệu vào bảng BookingRoomSlots (Liên kết đặt phòng & khung giờ)
INSERT INTO BookingRoomSlots (BookingId, RoomSlotId) VALUES
(1, 1),
(2, 2),
(3, 3);
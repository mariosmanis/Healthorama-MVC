CREATE TABLE PATIENT(
    patientAMKA VARCHAR(11) NOT NULL,
    patientID INT IDENTITY(1,1) NOT NULL,
    patientName VARCHAR(45) NOT NULL,
    patientSurname VARCHAR(45) NOT NULL,
    patientPassword VARCHAR(45) NOT NULL,
    PRIMARY KEY(patientAMKA),
    CONSTRAINT UC_PATIENTS UNIQUE(patientAMKA, patientID)
);

CREATE TABLE DOCTOR(
    doctorAMKA VARCHAR(11) UNIQUE NOT NULL,
    doctorName VARCHAR(45) NOT NULL,
    doctorSurname VARCHAR(45) NOT NULL,
    doctorUsername VARCHAR(45) NOT NULL,
    doctorSpeciality VARCHAR(45) NOT NULL,
    doctorPassword VARCHAR(45) NOT NULL,
    PRIMARY KEY(doctorAMKA)
);


CREATE TABLE DOCTOR_AVAILABILITY(
    ID INT IDENTITY(1, 1) NOT NULL,	
    doctorAMKA VARCHAR(11) NOT NULL,
    availabilityStart SMALLDATETIME NOT NULL,
    availabilityEnd SMALLDATETIME NOT NULL,
    FOREIGN KEY (doctorAMKA) REFERENCES DOCTOR(doctorAMKA) ON DELETE CASCADE,
	CONSTRAINT UC_DOCTOR_AVAILABILITY UNIQUE(doctorAMKA, availabilityStart, availabilityEnd),
    PRIMARY KEY(ID)   
)

CREATE TABLE APPOINTMENT(
    appointmentID INT IDENTITY(1,1) NOT NULL,
    appointmentTime SMALLDATETIME NOT NULL,
    patientAppointmentAMKA VARCHAR(11) NOT NULL,
    doctorAppointmentAMKA VARCHAR(11) NOT NULL,
    FOREIGN KEY (patientAppointmentAMKA) REFERENCES PATIENT(patientAMKA) ON DELETE CASCADE,
    FOREIGN KEY (doctorAppointmentAMKA) REFERENCES DOCTOR(doctorAMKA) ON DELETE CASCADE,
    PRIMARY KEY(appointmentID)  
);

CREATE TABLE ADMIN(
    adminID INT IDENTITY(1,1) NOT NULL,
    adminUsername VARCHAR(45) NOT NULL,
    adminPassword VARCHAR(45) NOT NULL,
    PRIMARY KEY(adminID),
    CONSTRAINT UC_ADMIN UNIQUE(adminID, adminPassword)
);


insert into ADMIN (adminUsername, adminPassword) values ('ewardale0', 'vIxWql');
insert into ADMIN (adminUsername, adminPassword) values ('mbertelet1', 'cGOJhAk6');
insert into ADMIN (adminUsername, adminPassword) values ('wpardie2', 'vxigN2dHUPCy');

insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('8207850698', 'Jacquelynn', 'Ouldcott', 'rLePnZcDR');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('4124564589', 'Sylvia', 'Doumer', 'w0ZtGh');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('5684055443', 'Pedro', 'Bulward', 'Eh48ZAXQlF');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('2434511686', 'Madison', 'Cattenach', 'BsXnkXRSnxE');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('0522974961', 'Townie', 'Maguire', 'nXQxTX');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('8287480868', 'Alfie', 'Illidge', 'viFSexh');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('9267373625', 'Jo', 'Bloys', 'e5grjqnRcA');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('2369731834', 'Worden', 'Dagger', 'cjoqj551Ou');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('9445446062', 'Darsie', 'MacCarrane', 'Kbpq5szVTg');
insert into PATIENT (patientAMKA, patientName, patientSurname, patientPassword) values ('8381380820', 'Gabbey', 'Stoffels', 'kgO6fI');


insert into DOCTOR (doctorSpeciality, doctorAMKA, doctorName, doctorSurname, doctorUsername, doctorPassword) values ('Ophthalmologist', '6571311348', 'Lee', 'Crowche', 'lcrowche0', 'Cm1qv08qM');
insert into DOCTOR (doctorSpeciality, doctorAMKA, doctorName, doctorSurname, doctorUsername, doctorPassword) values ('Pathologist', '7305599646', 'Samuel', 'MacDiarmid', 'smacdiarmid1', 'GH1Y6qaIQu');
insert into DOCTOR (doctorSpeciality, doctorAMKA, doctorName, doctorSurname, doctorUsername, doctorPassword) values ('Orthopedic', '7003311348', 'Addie', 'Carloni', 'acarloni2', 'LK4HfozC');


insert into DOCTOR_AVAILABILITY (doctorAMKA, availabilityStart, availabilityEnd) values ('6571311348', '2022/02/04 18:00:00', '2022/02/04 21:00:00');
insert into DOCTOR_AVAILABILITY (doctorAMKA, availabilityStart, availabilityEnd) values ('6571311348', '2022/02/14 16:00:00', '2022/02/14 21:00:00');
insert into DOCTOR_AVAILABILITY (doctorAMKA, availabilityStart, availabilityEnd) values ('7305599646', '2022/02/14 18:30:00', '2022/02/14 20:00:00');
insert into DOCTOR_AVAILABILITY (doctorAMKA, availabilityStart, availabilityEnd) values ('7003311348', '2022/02/24 19:00:00', '2022/02/24 22:00:00');


insert into APPOINTMENT (doctorAppointmentAMKA, patientAppointmentAMKA, appointmentTime) values ('7305599646', '4124564589', '2022/02/14 17:00:00');
insert into APPOINTMENT (doctorAppointmentAMKA, patientAppointmentAMKA, appointmentTime) values ('7003311348', '0522974961', '2022/02/24 18:30:00');
insert into APPOINTMENT (doctorAppointmentAMKA, patientAppointmentAMKA, appointmentTime) values ('6571311348', '2434511686', '2022/02/04 20:00:00');
insert into APPOINTMENT (doctorAppointmentAMKA, patientAppointmentAMKA, appointmentTime) values ('7305599646', '8381380820', '2022/03/11 18:30:00');


drop table admin;

drop table DOCTOR_AVAILABILITY;

drop table appointment;

drop table patient;

drop table doctor;
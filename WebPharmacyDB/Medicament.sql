CREATE TABLE [dbo].Medicament
(
	[MedicamentId]  INT  Identity(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(150) not null,
	Description nvarchar(300) not null,/*общее описание*/
	Price decimal not null,
	FirmId int not null,
	ImageUrl nvarchar(200) not null,
	ExpirationDate date not null,
	MedicamentTypeId int not null,
	FormulationId int not null, 
    CONSTRAINT [FK_Medicament_ToFirm] FOREIGN KEY (FirmId) REFERENCES Firm(Id), 
    CONSTRAINT [FK_Medicament_ToMedicamentType] FOREIGN KEY (MedicamentTypeId) REFERENCES MedicamentType(Id), 
    CONSTRAINT [FK_Medicament_Formulation] FOREIGN KEY (FormulationId) REFERENCES Formulation(Id) 





)

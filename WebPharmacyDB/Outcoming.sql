CREATE TABLE [dbo].Outcoming
(
	[Id] INT  Identity(1,1) NOT NULL PRIMARY KEY,
	MedicamentId int not null,
	Count int not null,
	Price decimal not null,
	OutcomedAt DateTime not null, 
    CONSTRAINT [FK_Outcoming_Medicament] FOREIGN KEY (MedicamentId) REFERENCES Medicament(MedicamentId)

)

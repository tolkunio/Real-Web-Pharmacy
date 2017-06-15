CREATE TABLE [dbo].[Storage]
(
	[StorageId] INT Identity(1,1) NOT NULL PRIMARY KEY,
	MedicamentId int not null,
	Count int not null, 
    CONSTRAINT [FK_Storage_Medicament] FOREIGN KEY (MedicamentId) REFERENCES Medicament(MedicamentId)
)

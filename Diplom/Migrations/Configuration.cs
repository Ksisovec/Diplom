namespace Diplom.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Diplom.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Diplom.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //IList<Departament> departaments = new List<Departament>();

            //departaments.Add(new Departament() { Name = "Балет" });
            //foreach (Departament dep in departaments)
            //    context.Departaments.AddOrUpdate(dep);

            context.Departaments.AddOrUpdate(
                p => p.Name,
                new Departament { Name = "Балет", ID = 1 }
                );
            context.Departaments.AddOrUpdate(
                 p => p.Name,
                 new Departament { Name = "Администрация", ID = 2 }
                 );

            IList<ArtistCategory> artistCategorys = new List<ArtistCategory>();
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 1,
                Name = "первая"
            });
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 2,
                Name = "вторая"
            });
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 3,
                Name = "третья"
            });
            artistCategorys.Add(new ArtistCategory()
            {
                ID = 4,
                Name = "не артист"
            });
            foreach (ArtistCategory artCat in artistCategorys)
                context.ArtistCategorys.AddOrUpdate(artCat);

            IList<ContractType> contractTypes = new List<ContractType>();
            contractTypes.Add(new ContractType()
            {
                ID = 1,
                Name = "стандартный"
            });
            contractTypes.Add(new ContractType()
            {
                ID = 2,
                Name = "продление"
            });
            foreach (ContractType conType in contractTypes)
                context.ContractTypes.AddOrUpdate(conType);

            IList<ConcertType> concertTypes = new List<ConcertType>();
            concertTypes.Add(new ConcertType()
            {
                ID = 1,
                Name = "сольный"
            });
            concertTypes.Add(new ConcertType()
            {
                ID = 2,
                Name = "участие"
            });
            foreach (ConcertType conType in concertTypes)
                context.ConcertTypes.AddOrUpdate(conType);

            IList<ConcertPlaceType> concertPlaceTypes = new List<ConcertPlaceType>();
            concertPlaceTypes.Add(new ConcertPlaceType()
            {
                ID = 1,
                Name = "домашний"
            });
            concertPlaceTypes.Add(new ConcertPlaceType()
            {
                ID = 2,
                Name = "выездной"
            });
            concertPlaceTypes.Add(new ConcertPlaceType()
            {
                ID = 3,
                Name = "заграничный"
            });
            foreach (ConcertPlaceType conPlaceType in concertPlaceTypes)
                context.ConcertPlaceTypes.AddOrUpdate(conPlaceType);


            IList<Worker> workers = new List<Worker>();

            workers.Add(new Worker()
            {
                Name = "Хостик",
                Surname = "Ласка",
                Patronymic = "Фиксович",
                RegistrationPlace = "Минск",
                BirthPlace = "Минск",
                DateOfBirth = new DateTime(1975, 10, 9, 0, 0, 0),
                Nationality = "Другое",
                Education = "высшее",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "4444444",
                Email = "mista@mail.ru",
                DepartamentID = 2,
                ID = 3
            }
            );
            workers.Add(new Worker()
            {
                Name = "Андрей",
                Surname = "Плюханов",
                Patronymic = "Факерович",
                RegistrationPlace = "Минск",
                BirthPlace = "Минск",
                DateOfBirth = new DateTime(1985, 10, 9, 0, 0, 0),
                Nationality = "Беларус",
                Education = "высшее",
                Sex = true,
                MaritalStatus = true,
                PhoneNum = "2022327",
                Email = "soska@mail.ru",
                DepartamentID = 1,
                ID = 1
            }
            );
            workers.Add(new Worker()
            {
                Name = "Клорис",
                Surname = "Амилуев",
                Patronymic = "Редвайтович",
                RegistrationPlace = "Минск",
                BirthPlace = "Киев",
                DateOfBirth = new DateTime(1966, 6, 6, 0, 0, 0),
                Nationality = "Русский",
                Education = "высшее",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "503819",
                Email = "klon_klopa@gmail.com",
                DepartamentID = 1,
                ID = 2
            });
            workers.Add(new Worker()
            {
                Name = "Халат",
                Surname = "Невр",
                Patronymic = "Изяславович",
                RegistrationPlace = "Минск",
                BirthPlace = "Москва",
                DateOfBirth = new DateTime(1988, 11, 25, 0, 0, 0),
                Nationality = "Русский",
                Education = "высшее",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "666123",
                Email = "Globeshar@gmail.com",
                DepartamentID = 1,
                ID = 4
            });
            workers.Add(new Worker()
            {
                Name = "Калх",
                Surname = "Гурниссон",
                Patronymic = "Бананович",
                RegistrationPlace = "Минск",
                BirthPlace = "Минск",
                DateOfBirth = new DateTime(1955, 1, 11, 0, 0, 0),
                Nationality = "Беларус",
                Education = "высшее",
                Sex = true,
                MaritalStatus = true,
                PhoneNum = "898956",
                Email = "keinthebanana@mail.com",
                DepartamentID = 2,
                ID = 5
            });
            workers.Add(new Worker()
            {
                Name = "Екатирина",
                Surname = "Мордаунт",
                Patronymic = "Кирьевна",
                RegistrationPlace = "Минск",
                BirthPlace = "Солигорс",
                DateOfBirth = new DateTime(1990, 2, 3, 0, 0, 0),
                Nationality = "Беларус",
                Education = "высшее",
                Sex = false,
                MaritalStatus = false,
                PhoneNum = "209901",
                Email = "GrayMizari@gmail.com",
                DepartamentID = 1,
                ID = 6
            });
            workers.Add(new Worker()
            {
                Name = "Андрей",
                Surname = "Садовников",
                Patronymic = "Александрович",
                RegistrationPlace = "Минск",
                BirthPlace = "Минск",
                DateOfBirth = new DateTime(1980, 5, 4, 0, 0, 0),
                Nationality = "Беларус",
                Education = "высшее",
                Sex = true,
                MaritalStatus = false,
                PhoneNum = "111222",
                Email = "Andreader@gmail.com",
                DepartamentID = 1,
                ID = 7
            });
            foreach (Worker wrk in workers)
                context.Workers.AddOrUpdate(wrk);



            IList<Contract> contracts = new List<Contract>();

            contracts.Add(new Contract()
            {
                ID = 6,
                ContractTypeId = 1,
                ArtistCategoryId = 3,
                WorkerId = 4,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 12, 0, 0, 0),
                OrderNum = 1116,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 7,
                ContractTypeId = 1,
                ArtistCategoryId = 4,
                WorkerId = 5,
                BeginningDate = new DateTime(2017, 2, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 2, 12, 0, 0, 0),
                OrderNum = 1117,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 8,
                ContractTypeId = 1,
                ArtistCategoryId = 2,
                WorkerId = 6,
                BeginningDate = new DateTime(2017, 1, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 1, 12, 0, 0, 0),
                OrderNum = 1118,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 9,
                ContractTypeId = 1,
                ArtistCategoryId = 4,
                WorkerId = 7,
                BeginningDate = new DateTime(2017, 4, 20, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 20, 0, 0, 0),
                OrderNum = 1119,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 5,
                ContractTypeId = 1,
                ArtistCategoryId = 4,
                WorkerId = 3,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 12, 0, 0, 0),
                OrderNum = 1115,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 1,
                ContractTypeId = 1,
                ArtistCategoryId = 1,
                WorkerId = 1,
                BeginningDate = new DateTime(2013, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2015, 4, 12, 0, 0, 0),
                OrderNum = 1111,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 2,
                ContractTypeId = 2,
                ArtistCategoryId = 2,
                WorkerId = 1,
                BeginningDate = new DateTime(2015, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2017, 4, 12, 0, 0, 0),
                OrderNum = 1112,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 3,
                ContractTypeId = 1,
                ArtistCategoryId = 3,
                WorkerId = 1,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2019, 4, 12, 0, 0, 0),
                OrderNum = 1113,
                Position = "Должность"
            });
            contracts.Add(new Contract()
            {
                ID = 4,
                ContractTypeId = 1,
                ArtistCategoryId = 3,
                WorkerId = 2,
                BeginningDate = new DateTime(2016, 5, 20, 0, 0, 0),
                EndDate = new DateTime(2018, 5, 20, 0, 0, 0),
                OrderNum = 1114,
                Position = "Должность"
            });
            foreach (Contract cont in contracts)
                context.Contracts.AddOrUpdate(cont);



            IList<ConcertEvent> concert = new List<ConcertEvent>();
            IList<ConcertMarks> concertMark = new List<ConcertMarks>();

            concert.Add(new ConcertEvent()
            {
                ID = 3,
                BeginningDate = new DateTime(2015, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2015, 5, 12, 0, 0, 0),
                Country = "Украина",
                City = "Киев",
                Address = "Майдан",
                ConcertTypeId = 2,
                ConcertPlaceTypeId = 3,
                Description = "Участие в митинге"
            });
            concert.Add(new ConcertEvent()
            {
                ID = 4,
                BeginningDate = new DateTime(2017, 2, 20, 0, 0, 0),
                EndDate = new DateTime(2017, 3, 5, 0, 0, 0),
                Country = "Китай",
                City = "Пекин",
                Address = "ул. Мао Цзе Дуна 8",
                ConcertTypeId = 1,
                ConcertPlaceTypeId = 3,
                Description = "Дни культуры беларуси в Китае"
            });
            concert.Add(new ConcertEvent()
            {
                ID = 5,
                BeginningDate = new DateTime(2016, 7, 22, 0, 0, 0),
                EndDate = new DateTime(2016, 7, 30, 0, 0, 0),
                Country = "Япония",
                City = "Токио",
                Address = "ул. Инио 12",
                ConcertTypeId = 2,
                ConcertPlaceTypeId = 3,
                Description = "Участие в комикете"
            });
            concert.Add(new ConcertEvent()
            {
                ID = 6,
                BeginningDate = new DateTime(2014, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2014, 5, 12, 0, 0, 0),
                Country = "Турция",
                City = "Анкара",
                Address = "пр. Карла Маркса 7",
                ConcertTypeId = 1,
                ConcertPlaceTypeId = 3,
                Description = "Дни культуры беларуси в Турции"
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 4,
                NumOfMarks = 60,
                ConcertEventID = 3,
                WorkerID = 1
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 5,
                NumOfMarks = 55,
                ConcertEventID = 3,
                WorkerID = 2
            });
            concert.Add(new ConcertEvent()
            {
                ID = 1,
                BeginningDate = new DateTime(2017, 4, 12, 0, 0, 0),
                EndDate = new DateTime(2017, 5, 12, 0, 0, 0),
                Country = "Турция",
                City = "Анкара",
                Address = "пр. Карла Маркса 7",
                ConcertTypeId = 1,
                ConcertPlaceTypeId = 3,
                Description = "Дни культуры беларуси в Турции"
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 1,
                NumOfMarks = 80,
                ConcertEventID = 1,
                WorkerID = 1
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 2,
                NumOfMarks = 60,
                ConcertEventID = 1,
                WorkerID = 2
            });
            concert.Add(new ConcertEvent()
            {
                ID = 2,
                BeginningDate = new DateTime(2016, 9, 12, 0, 0, 0),
                EndDate = new DateTime(2016, 9, 20, 0, 0, 0),
                Country = "Россия",
                City = "Москва",
                Address = "пр. Победителей 12",
                ConcertTypeId = 2,
                ConcertPlaceTypeId = 3,
                Description = "Учатие в дне города"
            });
            concertMark.Add(new ConcertMarks()
            {
                ID = 3,
                NumOfMarks = 25,
                ConcertEventID = 2,
                WorkerID = 1
            });

            foreach (ConcertEvent conc in concert)
                context.ConcertEvents.AddOrUpdate(conc);
            foreach (ConcertMarks concMark in concertMark)
                context.ConcertMarks.AddOrUpdate(concMark);
            base.Seed(context);

        }
    }
}

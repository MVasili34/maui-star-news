using NewsMobileApp.Models;
using NewsMobileApp.ViewModels;

namespace NewsMobileApp.TempServices;

public class NewsAppService : INewsService
{
    public IEnumerable<string> GetTags() =>
      new List<string>()
      {
            "#политика", //*
            "#история", //*
            "#образование", //*
            "#бизнес", //*
            "#маркетинг", //*
            "#природа", //*
            "#здоровье", //*
            "#спорт", //*
            "#кино", //*
            "#наука", //*
      };

    public IEnumerable<Section> GetCategories() =>
        new List<Section>()
        {
            new(1, "Политика", "\ue84f"),
            new(2, "История", "\ue153"),
            new(3, "Образование", "\ue80c"),
            new(4, "Бизнес", "\ue8f9"),
            new(5, "Маркетинг", "\ue8d1"),
            new(6, "Природа", "\ue545"),
            new(7, "Здоровье", "\ueb4c"),
            new(8, "Спорт", "\ue921"),
            new(9, "Кино", "\ue02c"),
            new(10, "Наука", "\uea4b"),
        };

    public IEnumerable<ArticlePreviewViewModel> GetLatestArticlesPreview() =>
    new List<ArticlePreviewViewModel>()
    {
            new()
            {
                ArticleId = new("67122fb6-8bef-43bb-9b8e-3352cd20e2e9"),
                Title = "Новые экономические санкции и тому подобный бред",
                Subtitle = "Влияние новых санкций на развитие экономики",
                Image = "https://cdnn21.img.ria.ru/images/130058/95/1300589535_0:0:2000:1126_600x0_80_0_0_c88706412ad64ecc7ab61be35d766373.jpg.webp",
                Section = new(4, "Бизнес", ""),
                PublishTime = new(2023, 2, 12, 12, 24, 10)
            },
            new()
            {
                ArticleId = new("dfb1ed29-9cfb-43d1-811e-ef6a7c13d8f7"),
                Title = "Новые победы гребцов и тому подобный бред",
                Subtitle = "Победы наших гребцов на румынских соревнованиях",
                Image = "https://picsum.photos/seed/four/300/200",
                Section = new(8, "Спорт", ""),
                PublishTime = new(2023, 4, 13, 10, 45, 12),
            },
            new()
            {
                ArticleId = new("d2a7a003-c69d-4d80-b475-954fd17d6c77"),
                Title = "Лекарство от смерти и тому подобный бред",
                Subtitle = "Найден новый способ отложить старость и снова помолодеть",
                Image = "https://a-tv.md/wp-content/uploads/2022/12/scale_12000333.png",
                Section = new(10, "Наука", ""),
                PublishTime = new(2023, 5, 23, 3, 23, 11),
            },
            new()
            {
                ArticleId = new("6bb62aa1-203e-4d09-b27e-dbeb023953f1"),
                Title = "Изменения в законе о ВО и тому подобный бред",
                Subtitle = "Предложены новые изменения в закон о Военной Обязонности ПМР",
                Image = "https://www.advgazeta.ru/upload/iblock/ed9/gosduma_prinyala_zakon_o_sozdanii_v_rossii_edinogo_reestra_voinskogo_ucheta_1.jpg",
                Section = new(1, "Политика", ""),
                PublishTime = new(2023, 4, 15, 13, 20, 11),
            },
    };

    public IEnumerable<ArticleViewModel> GetLatestArticlesFull() =>
        new List<ArticleViewModel>()
        {
            new()
            {
                ArticleId = new("67122fb6-8bef-43bb-9b8e-3352cd20e2e9"),
                Title = "Новые экономические санкции и тому подобный бред",
                Subtitle = "Влияние новых санкций на развитие экономики",
                Image = "https://cdnn21.img.ria.ru/images/130058/95/1300589535_0:0:2000:1126_600x0_80_0_0_c88706412ad64ecc7ab61be35d766373.jpg.webp",
                Views = 21,
                Text =  "   Введённые санкции включают в себя масштабные ограничения финансовой системы СтранаА (включая Центробанк и крупнейшие банки), " +
                        "деятельности ряда российских компаний и отдельных отраслей экономики, а также закрытие воздушного пространства и морских портов," +
                        " крупнейших предпринимателей.\n    Ограничительные меры за пособничество СтраныА также налагаются на СтрануВ и СтрануС",
                SectionId = 4,
                PublishTime = new(2023, 2, 12, 12, 24, 10)
            },
            new()
            {
                ArticleId = new("dfb1ed29-9cfb-43d1-811e-ef6a7c13d8f7"),
                Title = "Новые победы гребцов и тому подобный бред",
                Subtitle = "Победы наших гребцов на румынских соревнованиях",
                Image = "https://picsum.photos/seed/four/300/200",
                Views = 50,
                Text =  "Несмотря на сложные условия предсоревновательной подготовки — дожди, сильный ветер, дубоссарские спортсмены выступили очень хорошо." +
                        " Среди девушек две победы одержала Валентина Санду, два третьих места заняла Арина Шокодей. Среди юношей две победы на счету каноиста" +
                        " Дорина Страйстэ, Лев Лунин на дистанции 500м финишировал вторым.",
                SectionId = 8,
                PublishTime = new(2023, 4, 13, 10, 45, 12),
            },
            new()
            {
                ArticleId = new("d2a7a003-c69d-4d80-b475-954fd17d6c77"),
                Title = "Лекарство от смерти и тому подобный бред",
                Subtitle = "Найден новый способ отложить старость и снова помолодеть",
                Image = "https://a-tv.md/wp-content/uploads/2022/12/scale_12000333.png",
                Views = 31,
                Text = "Еженедельный авторитетный рецензируемый британский журнал The Lancet традиционно стоит на уровень выше большинства журналов, " +
                       "выпускаемых научными обществами и профессиональными ассоциациями. Это обусловлено тем, что он всегда делал и делает ставку на высокую" +
                       " объективность и разносторонность публикуемой научной информации. Журнал десятилетиями укреплял свою репутацию, публикуя, помимо сообщений " +
                       "о важнейших открытиях, исчерпывающие обзоры, посвященные различным проблемам медицинской практики. ",
                SectionId = 10,
                PublishTime = new(2023, 5, 23, 3, 23, 11),
            },
            new()
            {
                ArticleId = new("6bb62aa1-203e-4d09-b27e-dbeb023953f1"),
                Title = "Изменения в законе о ВО и тому подобный бред",
                Subtitle = "Предложены новые изменения в закон о Военной Обязонности ПМР",
                Image = "https://www.advgazeta.ru/upload/iblock/ed9/gosduma_prinyala_zakon_o_sozdanii_v_rossii_edinogo_reestra_voinskogo_ucheta_1.jpg",
                Views = 10,
                Text =  "ВС принял в третьем чтении пакет поправок к закону «О воинской обязанности и военной службе», его также одобрил Совет федерации." +
                        " Президент подписал его 14 апреля. Законопроект приравнивает электронные повестки к бумажным, а для уклонистов вводит запрет на выезд из страны," +
                        " управление транспортом и некоторые имущественные операции. По заявлению властей, нововведения касаются всех военнообязанных.",
                SectionId = 1,
                PublishTime = new(2023, 4, 15, 13, 20, 11),
            },
        };

    public IEnumerable<ArticlePreviewViewModel> GetRecommendedArticlesPreview() =>
        new List<ArticlePreviewViewModel>()
        {
            new()
            {
                ArticleId = new("2b9644bf-d116-4954-9ecf-1f82526ae69e"),
                Title = "Индексация зарплат",
                Subtitle = "Предлагается новый порядок индексации зарплат",
                Image = "https://picsum.photos/seed/nine/300/200",
                Section = new(4, "Бизнес", ""),
                PublishTime = new(2022, 2, 12, 21, 24, 10)
            },
            new()
            {
                ArticleId = new("997840fb-eb0c-4d39-b543-0f6b3ed291e8"),
                Title = "Новая информация о состоянии актёра",
                Subtitle = "Сводки о здоровье знаменитого актёра",
                Image = "https://i.imgur.com/HDBwFcy.jpg",
                Section = new(9, "Кино", ""),
                PublishTime = new(2023, 10, 19, 10, 21, 20),
            },
            new()
            {
                ArticleId = new("199f55fc-e4d6-4856-b958-e6eb67ca1907"),
                Title = "Юные атлеты",
                Subtitle = "Юниоры столичной спортивной школы одержали победы",
                Image = "https://picsum.photos/seed/ten/300/200",
                Section = new(8, "Спорт", ""),
                PublishTime = new(2022, 9, 13, 10, 45, 12),
            },
            new()
            {
                ArticleId = new("05ddbe67-31bb-4e3b-86fa-2ded70d42012"),
                Title = "Страхование",
                Subtitle = "Предлагается ввести системы больничного страхования",
                Image = "https://picsum.photos/seed/eleven/300/200",
                Section = new(10, "Наука", ""),
                PublishTime = new(2023, 6, 23, 3, 23, 11),
            },
            new()
            {
                ArticleId = new("b7facb83-c646-4652-99b1-aa0d33eb44fc"),
                Title = "Измения законов",
                Subtitle = "Предложены новые правки в законодательство",
                Image = "https://i.imgur.com/5uIu7zT.jpg",
                Section = new(1, "Политика", ""),
                PublishTime = new(2023, 1, 15, 13, 20, 11),
            },
        };

    public IEnumerable<ArticleViewModel> GetRecommendedArticlesFull() =>
    new List<ArticleViewModel>()
    {
            new()
            {
                ArticleId = new("2b9644bf-d116-4954-9ecf-1f82526ae69e"),
                Title = "Индексация зарплат",
                Subtitle = "Предлагается новый порядок индексации зарплат",
                Image = "https://picsum.photos/seed/nine/300/200",
                Views = 14,
                Text = "   Введённые санкции включают в себя масштабные ограничения финансовой системы СтранаА (включая Центробанк и крупнейшие банки), " +
                       "деятельности ряда российских компаний и отдельных отраслей экономики, а также закрытие воздушного пространства и морских портов," +
                       " крупнейших предпринимателей.\n    Ограничительные меры за пособничество СтраныА также налагаются на СтрануВ и СтрануС",
                SectionId = 4,
                PublishTime = new(2022, 2, 12, 21, 24, 10)
            },
            new()
            {
                ArticleId = new("997840fb-eb0c-4d39-b543-0f6b3ed291e8"),
                Title = "Новая информация о состоянии актёра",
                Subtitle = "Сводки о здоровье знаменитого актёра",
                Image = "https://i.imgur.com/HDBwFcy.jpg",
                Views = 20,
                Text =  "    Российский музыкальный продюсер Андрей Разин заявил о том, что испытывает страх за жизни своих коллег," +
                        " которых проклял из-за участия в концерте в честь 50-летия экс-солиста «Ласкового мая» Юрия Шатунова.\n" +
                        "    Об этом он сообщил в программе «Малахов» на канале «Россия».«Я только боюсь за этот концерт, что проклял их, " +
                        "теперь боюсь за них», — отметил Разин. Медиаменеджер поведал, что все, кто шли против него, уходили из жизни в нервных " +
                        "судорогах. «Обратимся к фактам. Юра Шатунов судился с Разиным — что дальше? Я сам себя боюсь. " +
                        "Все эти жертвы, о которых я предупреждал. Они меня не послушали», — добавил продюсер. При этом бывший участник коллектива" +
                        " «Ласковый май» Андрей Шишкин подчеркнул, что не боится проклятий медиаменеджера.\n" +
                        "    Ранее известный продюсер и музыкальный критик Павел Рудченко объяснил причины поведения экс-продюсера группы «Ласковый май»" +
                        " Андрея Разина, который угрожал проклятием артистам, боязнью остаться без денег.",
                SectionId = 9,
                PublishTime = new(2023, 10, 19, 10, 21, 20),
            },
            new()
            {
                ArticleId = new("199f55fc-e4d6-4856-b958-e6eb67ca1907"),
                Title = "Юные атлеты",
                Subtitle = "Юниоры столичной спортивной школы одержали победы",
                Image = "https://picsum.photos/seed/ten/300/200",
                Views = 30,
                Text = "Несмотря на сложные условия предсоревновательной подготовки — дожди, сильный ветер, дубоссарские спортсмены выступили очень хорошо." +
                       " Среди девушек две победы одержала Валентина Санду, два третьих места заняла Арина Шокодей. Среди юношей две победы на счету каноиста" +
                       " Дорина Страйстэ, Лев Лунин на дистанции 500м финишировал вторым.",
                SectionId = 8,
                PublishTime = new(2022, 9, 13, 10, 45, 12),
            },
            new()
            {
                ArticleId = new("05ddbe67-31bb-4e3b-86fa-2ded70d42012"),
                Title = "Страхование",
                Subtitle = "Предлагается ввести системы больничного страхования",
                Image = "https://picsum.photos/seed/eleven/300/200",
                Views = 25,
                Text = "Еженедельный авторитетный рецензируемый британский журнал The Lancet традиционно стоит на уровень выше большинства журналов, " +
                       "выпускаемых научными обществами и профессиональными ассоциациями. Это обусловлено тем, что он всегда делал и делает ставку на высокую" +
                       " объективность и разносторонность публикуемой научной информации. Журнал десятилетиями укреплял свою репутацию, публикуя, помимо сообщений " +
                       "о важнейших открытиях, исчерпывающие обзоры, посвященные различным проблемам медицинской практики. ",
                SectionId = 10,
                PublishTime = new(2023, 6, 23, 3, 23, 11),
            },
            new()
            {
                ArticleId = new("b7facb83-c646-4652-99b1-aa0d33eb44fc"),
                Title = "Измения законов",
                Subtitle = "Предложены новые правки в законодательство",
                Image = "https://i.imgur.com/5uIu7zT.jpg",
                Views = 30,
                Text = "ВС принял в третьем чтении пакет поправок к закону «О воинской обязанности и военной службе», его также одобрил Совет федерации." +
                       " Президент подписал его 14 апреля. Законопроект приравнивает электронные повестки к бумажным, а для уклонистов вводит запрет на выезд из страны," +
                       " управление транспортом и некоторые имущественные операции. По заявлению властей, нововведения касаются всех военнообязанных.",
                SectionId = 1,
                PublishTime = new(2023, 1, 15, 13, 20, 11),
            },
    };

    public IEnumerable<UserViewModel> GetUsers() =>
        new List<UserViewModel>()
        {
            new()
            {
                UserId = Guid.NewGuid(),
                UserName = "Vasika",
                EmailAddress = "vasika@mail.ru",
                Phone = "121323131",
                DateOfBirth = new(2001, 11, 5),
                PasswordSalt = "salt",
                PasswordHash = "sdsdadasdasXsa2112",
                RoleId = 1,
                Registered = DateTime.Now.AddDays(-10),
                LastLogin = DateTime.Now,
            },
            new()
            {
                UserId = Guid.NewGuid(),
                UserName = "Petka",
                EmailAddress = "petka@mail.ru",
                Phone = "45435211",
                DateOfBirth = new(2010, 2, 6),
                PasswordSalt = "salt2",
                PasswordHash = "sdsdadasdasXsa2112",
                RoleId = 2,
                Registered = DateTime.Now.AddDays(-12),
                LastLogin = DateTime.Now,
            },
            new()
            {
                UserId = Guid.NewGuid(),
                UserName = "Витёк Cергеев",
                EmailAddress = "vitok@mail.ru",
                Phone = "542144111",
                DateOfBirth = new(2005, 7, 2),
                PasswordSalt = "salt2",
                PasswordHash = "sdsdadasdasXsa2112",
                RoleId = 1,
                Registered = DateTime.Now.AddDays(-5),
                LastLogin = DateTime.Now,
            },
            new()
            {
                UserId = Guid.NewGuid(),
                UserName = "Ангелина Петрова",
                EmailAddress = "ignatiev2133@mail.ru",
                Phone = "43241941",
                DateOfBirth = new(2003, 8, 10),
                PasswordSalt = "salt3",
                PasswordHash = "sdsdadasdasXsa2112",
                RoleId = 1,
                Registered = DateTime.Now.AddDays(-3),
                LastLogin = DateTime.Now,
            },
            new()
            {
                UserId = Guid.NewGuid(),
                UserName = "Олегов Владислав",
                EmailAddress = "olegov.vlad12@mail.ru",
                Phone = "414324232",
                DateOfBirth = new(2002, 7, 2),
                PasswordSalt = "salt3",
                PasswordHash = "sdsdadasdasXsa2112",
                RoleId = 1,
                Registered = DateTime.Now.AddDays(-1),
                LastLogin = DateTime.Now,
            },
        };

    public IEnumerable<ArticlePreviewViewModel> GetThrendArticlesPreview() => GetRecommendedArticlesPreview().Concat(GetLatestArticlesPreview());
    public IEnumerable<ArticleViewModel> GetThrendArticlesFull() => GetRecommendedArticlesFull().Concat(GetLatestArticlesFull());
}

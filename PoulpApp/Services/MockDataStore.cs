using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoulpApp.Models;

namespace PoulpApp.Services
{
    public class MockDataStore : IDataStore<Beer>
    {
        readonly List<Beer> _items;

        public MockDataStore()
        {
            _items = new List<Beer>()
            {
                new Beer { Id = Guid.NewGuid().ToString(), Name="Corsedonk Rousse", Volume=33, Price=2.50, Degree=8, Color="Rousse", Quantity=46, PosterPath = new Uri("http://catalogue.vandb.fr/75-large_default/corsendonk-rousse-33cl.jpg"), Description="La Corsedonk rousse est une bière puissante qui comporte des touches sucrées, fruitées et douces. Une valeur sûre !"},
                new Beer { Id = Guid.NewGuid().ToString(), Name="Kasteel Triple", Volume=33, Price=3.00, Degree=11, Color="Blonde", Quantity=37, PosterPath = new Uri("http://catalogue.vandb.fr/148-thickbox_default/kasteel-triple-33cl.jpg"), Description="La Kasteel blonde synonyme de bière du château, porte une sublime robe dorée et arbore une mousse onctueuse. One note des pointes herbacés et de houblon. Au palais, le côté piquant et amère ne cache pas la douceur et la rondeur de la Kasteel."},
                new Beer { Id = Guid.NewGuid().ToString(), Name="Kasteel Brune", Volume=33, Price=3.00, Degree=11, Color="Brune", Quantity=54, PosterPath = new Uri("http://catalogue.vandb.fr/145-large_default/kasteel-brune-33cl.jpg"), Description="La Kasteel Brune puissante et forte laisse pénétrer les arômes sucrés. Visuellement, on pense inconsciement à un porto foncé..."},
                new Beer { Id = Guid.NewGuid().ToString(), Name="La Chouffe", Volume=33, Price=3.00, Degree=8, Color="Blonde", Quantity=43, PosterPath = new Uri("http://images.interdrinks.com/v5/img/p/1481-22222-w656-h450-tranparent.png"), Description="La Chouffe  présente une robe doré et légèrement trouble, avec un riche col de mousse blanc et de légères bulles. Cette bière de spécialité blonde révèle en bouche un caractère agréablement fruité, épicé à la coriandre avec une légère saveur houblonnée."},
                new Beer { Id = Guid.NewGuid().ToString(), Name="Super 8", Volume=33, Price=2.50, Degree=5, Color="Blanche", Quantity=21, PosterPath = new Uri("http://unepetitemousse.fr/images/box/juin-2018/super-8-blanche-haacht.jpg"), Description="Nez fruité, avec des notex de citron et d'écorces d'agrumes. Son attaque en bouche nous réserve des arômes plus herbacés et toujours axés sur les agrumes et le citronné. Lacoriandre marque vraiment la fin de bouche en lui apportant un bon côté épicée. Une blanche qui ne se laisse pas faire ! (houla...)"},
                new Beer { Id = Guid.NewGuid().ToString(), Name="Lindemans Kriek", Volume=33, Price=2.50, Degree=3.5, Color="Kriek", Quantity=24, PosterPath = new Uri("http://images.interdrinks.com/v5/img/p/736-14700-w656-h450-tranparent.png"), Description="Déliceieusement fruitée, avec la saveur fraîche des griottes fraîchement cueillies. Saveur vive et corsée, passant à un équilibre parfait de douceur et d'acidité."},
                new Beer { Id = Guid.NewGuid().ToString(), Name="Charles Quint", Volume=33, Price=3.00, Degree=8.5, Color="Blonde", Quantity=44, PosterPath = new Uri("http://images.interdrinks.com/v5/img/p/2676-15309-w656-h450-tranparent.png"), Description="La Charles Quint Keizer blonde est une bière blonde de haute fermentation brassée par la brasserie Hetanker en Belgique. Elle possède une robe blonde, des arômes de citron, d'abricot et de matl, et des notes mielleuses et fruitées au niveau du goût. Elle se boit généralement à une température de 8-10°C."},
                new Beer { Id = Guid.NewGuid().ToString(), Name="Charles Quint", Volume=33, Price=3.00, Degree=8.5, Color="Rubis", Quantity=11, PosterPath = new Uri("http://images.interdrinks.com/v5/img/p/346-13867-w656-h450-tranparent.png"), Description="Une bière ale crémeuse à la robe foncée rougeoyante de haute fermentation. Un corps généreux. Un arôme doux et fruité, un goût rond et moelleux, une première impression plaiusante et apaisanteà la fois. Une fin de bouche légèrement sucrée et délicatement houblonéee et des saveurs de prunes rouges en final. Se déguste avec plaisir et délicatesse. pour les épicuriens amateurs de fine mousse. Elle se boit généralement à une température de 8-10°C."},
                new Beer { Id = Guid.NewGuid().ToString(), Name="Cidre Magners", Volume=33, Price=3.00, Degree=4.5, Color="Brut", Quantity=37, PosterPath = new Uri("http://www.comptoir-irlandais.com/4785-thickbox_default/cidre-magners-original-33cl.jpg"), Description="Ce cidre irlandais possède une fraîcheur naturelle et une saveur unique au goût délicatement fruité de pommes fraîches et mûres avec des notes de chêne."},

            };
        }



        public async Task<bool> AddItemAsync(Beer item)
        {
            _items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Beer item)
        {
            var oldItem = _items.Where((Beer arg) => arg.Id == item.Id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.Where((Beer arg) => arg.Id == id).FirstOrDefault();
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Beer> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Beer>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }
    }
}
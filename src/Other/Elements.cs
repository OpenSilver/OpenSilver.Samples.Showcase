using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OpenSilver.Samples.Showcase
{
    internal class AtomicElements
    {
        public static PagedCollectionView Elements { get; set; } = new PagedCollectionView(GetListOfElements());
        private static ObservableCollection<Element> GetListOfElements()
        {
            return new ObservableCollection<Element>()
            {
                new Element("Hydrogen", "H"),
                new Element("Helium", "He"),
                new Element("Lithium", "Li"),
                new Element("Beryllium", "Be"),
                new Element("Boron", "B"),
                new Element("Carbon", "C"),
                new Element("Nitrogen", "N"),
                new Element("Oxygen", "O"),
                new Element("Fluorine", "F"),
                new Element("Neon", "Ne"),
                new Element("Sodium", "Na"),
                new Element("Magnesium", "Mg"),
                new Element("Aluminum", "Al"),
                new Element("Silicon", "Si"),
                new Element("Phosphorus", "P"),
                new Element("Sulfur", "S"),
                new Element("Chlorine", "Cl"),
                new Element("Argon", "Ar"),
                new Element("Potassium", "K"),
                new Element("Calcium", "Ca"),
                new Element("Scandium", "Sc"),
                new Element("Titanium", "Ti"),
                new Element("Vanadium", "V"),
                new Element("Chromium", "Cr"),
                new Element("Manganese", "Mn"),
                new Element("Iron", "Fe"),
                new Element("Cobalt", "Co"),
                new Element("Nickel", "Ni"),
                new Element("Copper", "Cu"),
                new Element("Zinc", "Zn"),
                new Element("Gallium", "Ga"),
                new Element("Germanium", "Ge"),
                new Element("Arsenic", "As"),
                new Element("Selenium", "Se"),
                new Element("Bromine", "Br"),
                new Element("Krypton", "Kr"),
                new Element("Rubidium", "Rb"),
                new Element("Strontium", "Sr"),
                new Element("Yttrium", "Y"),
                new Element("Zirconium", "Zr"),
                new Element("Niobium", "Nb"),
                new Element("Molybdenum", "Mo"),
                new Element("Technetium", "Tc"),
                new Element("Ruthenium", "Ru"),
                new Element("Rhodium", "Rh"),
                new Element("Palladium", "Pd"),
                new Element("Silver", "Ag"),
                new Element("Cadmium", "Cd"),
                new Element("Indium", "In"),
                new Element("Tin", "Sn"),
                new Element("Antimony", "Sb"),
                new Element("Tellurium", "Te"),
                new Element("Iodine", "I"),
                new Element("Xenon", "Xe"),
                new Element("Cesium", "Cs"),
                new Element("Barium", "Ba"),
                new Element("Lanthanum", "La"),
                new Element("Cerium", "Ce"),
                new Element("Praseodymium", "Pr"),
                new Element("Neodymium", "Nd"),
                new Element("Promethium", "Pm"),
                new Element("Samarium", "Sm"),
                new Element("Europium", "Eu"),
                new Element("Gadolinium", "Gd"),
                new Element("Terbium", "Tb"),
                new Element("Dysprosium", "Dy"),
                new Element("Holmium", "Ho"),
                new Element("Erbium", "Er"),
                new Element("Thulium", "Tm"),
                new Element("Ytterbium", "Yb"),
                new Element("Lutetium", "Lu"),
                new Element("Hafnium", "Hf"),
                new Element("Tantalum", "Ta"),
                new Element("Tungsten", "W"),
                new Element("Rhenium", "Re"),
                new Element("Osmium", "Os"),
                new Element("Iridium", "Ir"),
                new Element("Platinum", "Pt"),
                new Element("Gold", "Au"),
                new Element("Mercury", "Hg"),
                new Element("Thallium", "Tl"),
                new Element("Lead", "Pb"),
                new Element("Bismuth", "Bi"),
                new Element("Polonium", "Po"),
                new Element("Astatine", "At"),
                new Element("Radon", "Rn"),
                new Element("Francium", "Fr"),
                new Element("Radium", "Ra"),
                new Element("Actinium", "Ac"),
                new Element("Thorium", "Th"),
                new Element("Protactinium", "Pa"),
                new Element("Uranium", "U"),
                new Element("Neptunium", "Np"),
                new Element("Plutonium", "Pu"),
                new Element("Americium", "Am"),
                new Element("Curium", "Cm"),
                new Element("Berkelium", "Bk"),
                new Element("Californium", "Cf"),
                new Element("Einsteinium", "Es"),
                new Element("Fermium", "Fm"),
                new Element("Mendelevium", "Md"),
                new Element("Nobelium", "No"),
                new Element("Lawrencium", "Lr"),
                new Element("Rutherfordium", "Rf"),
                new Element("Dubnium", "Db"),
                new Element("Seaborgium", "Sg"),
                new Element("Bohrium", "Bh"),
                new Element("Hassium", "Hs"),
                new Element("Meitnerium", "Mt"),
                new Element("Darmstadtium", "Ds"),
                new Element("Roentgenium", "Rg"),
                new Element("Copernicium", "Cn"),
                new Element("Nihonium", "Nh"),
                new Element("Flerovium", "Fl"),
                new Element("Moscovium", "Mc"),
                new Element("Livermorium", "Lv"),
                new Element("Tennessine", "Ts"),
                new Element("Oganesson", "Og")
            };
            
        }



    }



    class Element
    {
        public string Name { get; }
        public string Symbol { get; }

        public Element(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }
    }
}

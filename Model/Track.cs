using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = ArrayToLinkedList(sections);
        }

        public Track(string v, LinkedList<Section> section)
        {
        }

        public LinkedList<Section> ArrayToLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> sectionList = new LinkedList<Section>();
            foreach(SectionTypes section in sections)
            {
                sectionList.AddLast(new Section() { SectionType = section });
            }
            return sectionList;
        }
    }
}

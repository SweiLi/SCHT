
namespace BDH.IView
{
    public enum PictureLayouts
    {
        JustOne, UpDown, LeftRight, UpOneDownTwo, LeftUp, RightUp, LeftDown, RightDown,
        LeftOneRightTwo, FourSquare, ThreeVertical, ThreeHorizontal, UpOneDownThree,
        RightUpTwo, FiveVertical
    }

    public static class PictureLayoutHelper
    {
        public static int GetMinImageCount(PictureLayouts pl)
        {
            int count = 1;

            switch (pl)
            {
                case PictureLayouts.JustOne:
                    count = 1;
                    break;
                case PictureLayouts.UpDown:
                case PictureLayouts.LeftRight:
                case PictureLayouts.LeftUp:
                case PictureLayouts.RightUp:
                case PictureLayouts.LeftDown:
                case PictureLayouts.RightDown:
                    count = 2;
                    break;
                case PictureLayouts.UpOneDownTwo:
                case PictureLayouts.LeftOneRightTwo:
                case PictureLayouts.ThreeHorizontal:
                case PictureLayouts.ThreeVertical:
                case PictureLayouts.RightUpTwo:
                    count = 3;
                    break;
                case PictureLayouts.FourSquare:
                case PictureLayouts.UpOneDownThree:
                    count = 4;
                    break;
                case PictureLayouts.FiveVertical:
                    count = 4;
                    break;
            }

            return count;
        }
    }
}

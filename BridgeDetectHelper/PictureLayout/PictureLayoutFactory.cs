using BDH.IView;

namespace BridgeDetectHelper.PictureLayout
{
    /// <summary>
    /// 图片排版创建器工厂类，创建实例化图片排版接口
    /// </summary>
    public class PictureLayoutFactory
    {
        public static IPictureLayout GetCreator(PictureLayouts picLayout)
        {
            IPictureLayout pic_layout = null;

            switch (picLayout)
            {
                case PictureLayouts.JustOne:
                    pic_layout = new PictureLayoutJustOne();
                    break;
                case PictureLayouts.UpDown:
                    pic_layout = new PictureLayoutUpDown();
                    break;
                case PictureLayouts.LeftRight:
                    pic_layout = new PictureLayoutLeftRight();
                    break;
                case PictureLayouts.UpOneDownTwo:
                    pic_layout = new PictureLayoutUpOneDownTwo();
                    break;
                case PictureLayouts.LeftUp:
                    pic_layout = new PictureLayoutLeftUp();
                    break;
                case PictureLayouts.RightUp:
                    pic_layout = new PictureLayoutRightUp();
                    break;
                case PictureLayouts.LeftDown:
                    pic_layout = new PictureLayoutLeftDown();
                    break;
                case PictureLayouts.RightDown:
                    pic_layout = new PictureLayoutRightDown();
                    break;
                case PictureLayouts.LeftOneRightTwo:
                    pic_layout = new PictureLayoutLeftOneRightTwo();
                    break;
                case PictureLayouts.FourSquare:
                    pic_layout = new PictureLayoutFourSquare();
                    break;
                case PictureLayouts.ThreeVertical:
                    pic_layout = new PictureLayoutThreeVertical();
                    break;
                case PictureLayouts.ThreeHorizontal:
                    pic_layout = new PictureLayoutThreeHorizontal();
                    break;
                case PictureLayouts.UpOneDownThree:
                    pic_layout = new PictureLayoutUpOneDownThree();
                    break;
                case PictureLayouts.RightUpTwo:
                    pic_layout = new PictureLayoutRightUpTwo();
                    break;
                case PictureLayouts.FiveVertical:
                    pic_layout = new PictureLayoutFiveVertical();
                    break;
                default:
                    pic_layout = null;
                    break;
            }

            return pic_layout;
        }
    }
}

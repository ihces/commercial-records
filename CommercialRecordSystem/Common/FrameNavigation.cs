using CommercialRecordSystem.ViewModels;
using System;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Common
{
    class FrameNavigation
    {
        #region Properties
        private Frame pageFrame = null;
        public Frame PageFrame
        {
            get
            {
                return pageFrame;
            }
            set
            {
                pageFrame = value;
            }
        }

        private Type pageType = null;
        public Type PageType
        {
            get
            {
                return pageType;
            }
        }

        private object message = null;
        public object Message 
        {
            get
            {
                return message;
            }
        }

        private FrameNavigation back = null;
        public FrameNavigation Back
        { 
            get
            {
                return back;
            }
            set
            {
                back = value;
            }
        }
        
        private FrameNavigation forward = null;
        public FrameNavigation Forward
        {
            get
            {
                return forward;
            }
            set
            {
                forward = value;
            }
        }

        public bool CanGoBack
        {
            get
            {
                if (back != null)
                    return true;
                return false;
            }
        }
        public bool CanGoForward
        {
            get
            {
                if (forward != null)
                    return true;
                return false;
            }
        }
        #endregion

        public FrameNavigation(Type pageType)
        {
            this.pageType = pageType;
        }

        public virtual void GoBack(object message = null)
        {
            if (CanGoBack)
            {
                back.forward = this;
                back.message = message;
                pageFrame.Navigate(back.pageType, back);
            }
        }

        public virtual void GoForward(object message = null)
        {
            if (CanGoForward)
            {
                forward.back = this;
                forward.message = message;
                pageFrame.Navigate(forward.pageType, forward);
            }
        }

        public bool Navigate<T>(object parameter = null)
        {
            var type = typeof(T);

            return Navigate(type, parameter);
        }

        public bool Navigate(Type source, object parameter = null)
        {
            FrameNavigation frameNavi = new FrameNavigation(source);
            frameNavi.message = parameter;
            frameNavi.back = this;
            return pageFrame.Navigate(source, frameNavi);
        }

        public bool Is<T>()
        {
            return pageType.Equals(typeof(T));
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MTQtoolEditor.Controls.Helpers;

namespace MTQtoolEditor.Controls
{
    public partial class PanelHeader : UserControl
    {
        private const string FallbackHideIcon = "mdi-arrow-expand";

        public enum HideDirectionType
        {
            Left,
            Right,
            Bottom
        }
        
        private string _title = "";
        private HideDirectionType _hideDirection = HideDirectionType.Left;
        private bool _isCollapsed = false;
        private GridSplitter? _splitter;
        private Visual? _content;
        private string? _icon;
        private Grid? _gridLink;
        private int? _definitionIndex;
        private string _hideDirectionIcon = FallbackHideIcon;
        private bool _isChangeOrientation;

        public static readonly DirectProperty<PanelHeader, string> TitleProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, string>(
                nameof(Title),
                o => o.Title,
                (o, v) => o.Title = v);
        
        public static readonly DirectProperty<PanelHeader, HideDirectionType> HideDirectionProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, HideDirectionType>(
                nameof(HideDirection),
                o => o.HideDirection,
                (o, v) => o.HideDirection = v);
        
        public static readonly DirectProperty<PanelHeader, bool> IsCollapsedProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, bool>(
                nameof(IsCollapsed),
                o => o.IsCollapsed,
                (o, v) => o.IsCollapsed = v);
        
        public static readonly DirectProperty<PanelHeader, GridSplitter?> SplitterProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, GridSplitter?>(
                nameof(Splitter),
                o => o.Splitter,
                (o, v) => o.Splitter = v);
        
        public static readonly DirectProperty<PanelHeader, Visual?> ContentVisualProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, Visual?>(
                nameof(ContentVisual),
                o => o.ContentVisual,
                (o, v) => o.ContentVisual = v);
        
        public static readonly DirectProperty<PanelHeader, string?> IconProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, string?>(
                nameof(Icon),
                o => o.Icon,
                (o, v) => o.Icon = v);

        public static readonly DirectProperty<PanelHeader, Grid?> GridLinkProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, Grid?>(
                nameof(GridLink),
                o => o.GridLink,
                (o, v) => o.GridLink = v);
        
        public static readonly DirectProperty<PanelHeader, int?> DefinitionIndexProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, int?>(
                nameof(DefinitionIndex),
                o => o.DefinitionIndex,
                (o, v) => o.DefinitionIndex = v);
        
        protected static readonly DirectProperty<PanelHeader, string> HideDirectionIconProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, string>(
                nameof(HideDirectionIcon),
                o => o.HideDirectionIcon,
                (o, v) => o.HideDirectionIcon = v);
        
        protected static readonly DirectProperty<PanelHeader, bool> IsChangeOrientationProperty =
            AvaloniaProperty.RegisterDirect<PanelHeader, bool>(
                nameof(IsChangeOrientation),
                o => o.IsChangeOrientation,
                (o, v) => o.IsChangeOrientation = v);

        #region PublicProperties

        public string Title
        {
            get => _title;
            set => SetAndRaise(TitleProperty, ref _title, value);
        }

        public HideDirectionType HideDirection
        {
            get => _hideDirection;
            set
            {
                SetAndRaise(HideDirectionProperty, ref _hideDirection, value);
                SetAndRaise(HideDirectionIconProperty, ref _hideDirectionIcon, GetIcon(value));
            }
        }

        public bool IsCollapsed
        {
            get => _isCollapsed;
            set
            {
                SetAndRaise(IsCollapsedProperty, ref _isCollapsed, value);
                SetAndRaise(HideDirectionIconProperty, ref _hideDirectionIcon, GetIcon(_hideDirection));

                if (_content != null)
                {
                    _content.IsVisible = !value;
                }

                if (_splitter != null)
                {
                    _splitter.IsEnabled = !value;
                }

                if (_gridLink != null && _definitionIndex != null)
                {
                    if (_hideDirection is HideDirectionType.Left or HideDirectionType.Right) // Columns
                    {
                        var cd = _gridLink.ColumnDefinitions;
                        var definition = cd[_definitionIndex.Value];
                        
                        if (!value) // Expand
                        {
                            IsChangeOrientation = false;
                            definition.Width = GridLength.Star;
                            
                            if (GridSizeHolder.GetMinSize(definition) is { } mw)
                            {
                                definition.MinWidth = mw;
                            }

                            if (GridSizeHolder.GetActualSize(definition) is { } aw)
                            {
                                // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                                definition.Width = GridLength.Parse(aw.ToString());
                            }

                            _gridLink.ColumnDefinitions = cd;
                        }
                        else // Collapse
                        {
                            IsChangeOrientation = true;
                            GridSizeHolder.SetSize(definition);
                            definition.Width = GridLength.Parse("36");
                            definition.MinWidth = 0;
                            _gridLink.ColumnDefinitions = cd;
                        }
                    }
                    else // Rows
                    {
                        var rd = _gridLink.RowDefinitions;
                        var definition = rd[_definitionIndex.Value];
                        
                        if (!value) // Expand
                        {
                            definition.Height = GridLength.Star;

                            if (GridSizeHolder.GetMinSize(definition) is { } mh)
                            {
                                definition.MinHeight = mh;
                            }

                            if (GridSizeHolder.GetActualSize(definition) is { } ah)
                            {
                                // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                                definition.Height = GridLength.Parse(ah.ToString());
                            }

                            _gridLink.RowDefinitions = rd;
                        }
                        else // Collapse
                        {
                            GridSizeHolder.SetSize(definition);
                            definition.Height = GridLength.Parse("36");
                            definition.MinHeight = 0;
                            _gridLink.RowDefinitions = rd;
                        }
                    }
                }
            }
        }

        public GridSplitter? Splitter
        {
            get => _splitter;
            set => SetAndRaise(SplitterProperty, ref _splitter, value);
        }
        
        public Visual? ContentVisual
        {
            get => _content;
            set => SetAndRaise(ContentVisualProperty, ref _content, value);
        }
        
        public string? Icon
        {
            get => _icon;
            set => SetAndRaise(IconProperty, ref _icon, value);
        }

        public Grid? GridLink
        {
            set => SetAndRaise(GridLinkProperty, ref _gridLink, value);
            get => _gridLink;
        }

        public int? DefinitionIndex
        {
            set => SetAndRaise(DefinitionIndexProperty, ref _definitionIndex, value);
            get => _definitionIndex;
        }

        #endregion

        #region ProtectedProperties

        protected string HideDirectionIcon
        {
            set => SetAndRaise(HideDirectionIconProperty, ref _hideDirectionIcon, value);
            get => _hideDirectionIcon;
        }

        protected bool IsChangeOrientation
        {
            set => SetAndRaise(IsChangeOrientationProperty, ref _isChangeOrientation, value);
            get => _isChangeOrientation;
        }

        #endregion
        
        public PanelHeader()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            AffectsMeasure<PanelHeader>(TitleProperty);
        }

        private string GetIcon(HideDirectionType type)
        {
            return type switch
            {
                HideDirectionType.Left => _isCollapsed ? "mdi-arrow-expand-right" : "mdi-arrow-expand-left",
                HideDirectionType.Right => _isCollapsed ? "mdi-arrow-expand-left" : "mdi-arrow-expand-right",
                HideDirectionType.Bottom => _isCollapsed ? "mdi-arrow-expand-up" : "mdi-arrow-expand-down",
                _ => FallbackHideIcon
            };
        }

        private void ToggleState(object? sender, RoutedEventArgs e)
        {
            IsCollapsed = !_isCollapsed;
        }
    }
}
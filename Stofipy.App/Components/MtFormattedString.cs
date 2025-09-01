using System.ComponentModel;
using Microsoft.Maui.Handlers;

namespace Stofipy.App.Components;

/// <summary>
/// This exists only due to the fact that <see cref="Span"/>s do not inherit <see cref="Style"/>s from their parent <see cref="Label"/>.
/// Usage is enforced by a custom <see cref="LabelHandler"/> mapping which throws <see cref="InvalidOperationException"/> for standard <see cref="FormattedString"/>
/// </summary>
public class MtFormattedString : FormattedString, IDisposable
{
    private static readonly BindableProperty[] BindableProperties =
    [
        Label.FontFamilyProperty,
        Label.FontSizeProperty,
        Label.LineHeightProperty,
        Label.CharacterSpacingProperty
    ];

    protected override void OnParentSet()
    {
        ApplyStyle();
        if (Parent is not null)
        {
            Parent.PropertyChanged += ParentOnPropertyChanged;
        }
    }

    private void ApplyStyle()
    {
        if (Parent is not Label parentLabel)
        {
            return;
        }

        List<Setter> parentSetters = FindParentSetters(parentLabel.Style, BindableProperties);

        ApplyParentLabelProperties(parentLabel, parentSetters);

        foreach (Span span in Spans)
        {
            Style s = span.Style ?? new Style(targetType: typeof(Span));
            foreach (var setter
                     in parentSetters.Where(setter => s.Setters
                                                .Any(existingSetter => existingSetter.Property.PropertyName == setter.Property.PropertyName) is false))
            {
                s.Setters.Add(setter);
            }

            span.Style = null;
            span.Style = s;
        }
    }

    private void ParentOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(Style) && e.PropertyName != nameof(Label.FontFamily))
        {
            return;
        }
        ApplyStyle();
    }

    protected override void OnParentChanging(ParentChangingEventArgs args)
    {
        base.OnParentChanging(args);
        if (args.OldParent is not null)
        {
            args.OldParent.PropertyChanged -= ParentOnPropertyChanged;
        }
        if (args.NewParent is not null)
        {
            args.NewParent.PropertyChanged += ParentOnPropertyChanged;
        }
    }

    private static List<Setter> FindParentSetters(Style? style, BindableProperty[] properties)
    {
        List<Setter> setters = [];
        if (style is null)
        {
            return setters;
        }
        if (style.BasedOn is not null)
        {
            setters.AddRange(FindParentSetters(style.BasedOn, properties));
        }
        if (style.Setters is not null)
        {
            foreach (BindableProperty property in properties)
            {
                Setter? setter = style.Setters.FirstOrDefault(s => s.Property == property);
                if (setter is not null)
                {
                    var existingSetter = setters.FirstOrDefault(s => s.Property.PropertyName == property.PropertyName);
                    if (existingSetter is not null)
                    {
                        existingSetter.Value = setter.Value;
                    }
                    else
                    {
                        setters.Add(new Setter { Property = setter.Property, Value = setter.Value, TargetName = setter.TargetName});
                    }
                }
            }
        }

        return setters;
    }

    private void ApplyParentLabelProperties(Label parentLabel, List<Setter> parentSetters)
    {
        if (parentLabel.FontFamily is not null)
        {
            var existingFontFamilySetter = parentSetters.FirstOrDefault(s => s.Property == Label.FontFamilyProperty);
            if (existingFontFamilySetter is not null)
            {
                existingFontFamilySetter.Value = parentLabel.FontFamily;
            }
            else
            {
                parentSetters.Add(new Setter
                {
                    Property = Label.FontFamilyProperty,
                    Value = parentLabel.FontFamily
                });
            }
        }
    }

    public void Dispose()
    {
        if(Parent is not null)
        {
            Parent.PropertyChanged -= ParentOnPropertyChanged;
        }

        GC.SuppressFinalize(this);
    }
}

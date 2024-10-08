﻿using Eudaimonia.Domain.Kernel;
using Eudaimonia.Domain.Validation;

namespace Eudaimonia.Domain;

public class Language : ValueObject<Language>
{
    private static readonly HashSet<string> _languageCodes = new()
    {
        "aa","ab","af","ak","am","an","ar","as","av","ay","az","ba","be","bg","bh","bi","bm","bn","bo","br","bs","ca","ce","ch","co","cr","cs","cv","cy",
        "da","de","dv","dz","ee","el","en","es","et","eu","fa","ff","fi","fj","fo","fr","fy","ga","gd","gl","gn","gu","gv","ha","he","hi","ho","hr","ht",
        "hu","hy","hz","id","ig","ii","ik","io","is","it","iu","ja","jv","ka","kg","ki","kj","kk","kl","km","kn","ko","kr","ks","ku","kv","kw","ky","lb",
        "lg","li","ln","lo","lt","lu","lv","mg","mh","mi","mk","ml","mn","mr","ms","mt","my","na","nb","nd","ne","ng","nl","nn","no","nr","nv","ny","oc",
        "oj","om","or","os","pa","pl","ps","pt","qu","rm","rn","ro","ru","rw","sa","sc","sd","se","sg","si","sk","sl","sm","sn","so","sq","sr","ss","st",
        "su","sv","sw","ta","te","tg","th","ti","tk","tl","tn","to","tr","ts","tt","tw","ty","ug","uk","ur","uz","ve","vi","wa","wo","xh","yi","yo","za",
        "zh","zu",
    };

    public string Code { get; }

    public Language(string code)
    {
        Code = code;

        ThrowIfInvalid();
    }

    protected override List<ValidationError> Validate()
    {
        var errors = base.Validate();

        if (!_languageCodes.Contains(Code))
            AddError(errors, nameof(Code), "Code must be a valid ISO 639-1 code.");

        return errors;
    }
}
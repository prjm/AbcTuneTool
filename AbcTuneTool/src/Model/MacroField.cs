﻿using AbcTuneTool.Model.Fields;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     macro definition
    /// </summary>
    public class MacroField : InformationField {

        /// <summary>
        ///     create a new macro field
        /// </summary>
        /// <param name="header"></param>
        /// <param name="fieldValues"></param>
        public MacroField(Terminal header, Terminal fieldValues) : base(header, fieldValues, InformationFieldKind.Macro) {
        }
    }
}

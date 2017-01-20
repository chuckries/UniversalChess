using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Common;
using UniversalChess.Model;

namespace UniversalChess.UI.Controls
{
    class ChessSquare : BindableBase
    {
        public int Id { get; private set; }

        public Piece Piece { get; set; }

        public ChessSquare(int id, Piece piece)
        {
            Id = id;
            Piece = piece;
        }
    }
}

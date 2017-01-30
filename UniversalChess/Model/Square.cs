using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Common;

namespace UniversalChess.Model
{
    public class Square : BindableBase
    {
        private int _id;
        private Piece _piece;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public Piece Piece
        {
            get { return _piece; }
            set { SetProperty(ref _piece, value); }
        }
    }
}

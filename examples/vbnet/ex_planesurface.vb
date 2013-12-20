﻿Imports Rhino
Imports Rhino.Geometry
Imports Rhino.Commands

Namespace examples_vb
  <System.Runtime.InteropServices.Guid("742C3173-B25A-4C43-9BB9-8636C26928AC")> _
  Public Class PlaneSurfaceCommand
    Inherits Command
    Public Overrides ReadOnly Property EnglishName() As String
      Get
        Return "vbPlaneSurface"
      End Get
    End Property

    Protected Overrides Function RunCommand(doc As RhinoDoc, mode As RunMode) As Result
      Dim corners As Point3d()
      Dim rc = Rhino.Input.RhinoGet.GetRectangle(corners)
      If rc <> Result.Success Then
        Return rc
      End If

      Dim plane = New Plane(corners(0), corners(1), corners(2))
      Dim plane_surface = New PlaneSurface(plane, New Interval(0, corners(0).DistanceTo(corners(1))), New Interval(0, corners(1).DistanceTo(corners(2))))
      doc.Objects.Add(plane_surface)
      doc.Views.Redraw()
      Return Result.Success
    End Function
  End Class
End Namespace
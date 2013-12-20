﻿Imports System.Linq
Imports Rhino
Imports Rhino.DocObjects
Imports Rhino.Geometry
Imports Rhino.Commands
Imports Rhino.Input

Namespace examples_vb
  <System.Runtime.InteropServices.Guid("76683BCC-8A16-4E5A-A87F-B32DB885629A")> _
  Public Class IsPlanarSurfaceInPlaneCommand
    Inherits Command
    Public Overrides ReadOnly Property EnglishName() As String
      Get
        Return "vbIsPlanarSurfaceInPlane"
      End Get
    End Property

    Protected Overrides Function RunCommand(doc As RhinoDoc, mode As RunMode) As Result
      Dim obj_ref As ObjRef
      Dim rc = RhinoGet.GetOneObject("select surface", True, ObjectType.Surface, obj_ref)
      If rc <> Result.Success Then
        Return rc
      End If
      Dim surface = obj_ref.Surface()

      Dim corners As Point3d()
      rc = Rhino.Input.RhinoGet.GetRectangle(corners)
      If rc <> Result.Success Then
        Return rc
      End If

      Dim plane = New Plane(corners(0), corners(1), corners(2))

      Dim is_or_isnt = If(IsSurfaceInPlane(surface, plane, doc.ModelAbsoluteTolerance), "", " not ")
      RhinoApp.WriteLine(String.Format("Surface is{0} in plane.", is_or_isnt))
      Return Result.Success
    End Function

    Private Function IsSurfaceInPlane(surface As Surface, plane As Plane, tolerance As Double) As Boolean
      If Not surface.IsPlanar(tolerance) Then
        Return False
      End If

      Dim bbox = surface.GetBoundingBox(True)
      Return bbox.GetCorners().All(Function(corner) System.Math.Abs(plane.DistanceTo(corner)) <= tolerance)
    End Function
  End Class
End Namespace
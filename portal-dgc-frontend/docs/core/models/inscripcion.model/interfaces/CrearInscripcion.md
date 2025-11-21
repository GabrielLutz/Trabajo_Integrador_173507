[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/models/inscripcion.model](../README.md) / CrearInscripcion

# Interface: CrearInscripcion

Defined in: [src/app/core/models/inscripcion.model.ts:4](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/inscripcion.model.ts#L4)

Payload utilizado en RF-05 para iniciar una nueva inscripción.

## Properties

### apoyosIds

> **apoyosIds**: `number`[]

Defined in: [src/app/core/models/inscripcion.model.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/inscripcion.model.ts#L16)

Apoyos solicitados por el postulante para el proceso de evaluación.

***

### autodefinicion

> **autodefinicion**: [`AutodefinicionLey`](AutodefinicionLey.md)

Defined in: [src/app/core/models/inscripcion.model.ts:10](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/inscripcion.model.ts#L10)

Autodefinición proporcionada en cumplimiento de la Ley 19.122 (RF-08).

***

### departamentoId

> **departamentoId**: `number`

Defined in: [src/app/core/models/inscripcion.model.ts:8](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/inscripcion.model.ts#L8)

Departamento donde se realiza la inscripción.

***

### llamadoId

> **llamadoId**: `number`

Defined in: [src/app/core/models/inscripcion.model.ts:6](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/inscripcion.model.ts#L6)

Identificador del llamado seleccionado en el paso 1.

***

### meritos

> **meritos**: [`MeritoPostulante`](MeritoPostulante.md)[]

Defined in: [src/app/core/models/inscripcion.model.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/inscripcion.model.ts#L14)

Méritos y antecedentes adjuntados para evaluación.

***

### requisitos

> **requisitos**: [`RequisitoPostulante`](RequisitoPostulante.md)[]

Defined in: [src/app/core/models/inscripcion.model.ts:12](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/inscripcion.model.ts#L12)

Requisitos declarados por el postulante durante la carga (RF-07).

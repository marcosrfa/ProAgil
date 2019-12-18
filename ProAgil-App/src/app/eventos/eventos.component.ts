import { Component, OnInit, TemplateRef } from "@angular/core";
import { Evento } from "../_models/Evento";
import { EventoService } from "../_services/evento.service";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { defineLocale, BsLocaleService, ptBrLocale } from "ngx-bootstrap";
defineLocale("pt-br", ptBrLocale);

@Component({
  selector: "app-eventos",
  templateUrl: "./eventos.component.html",
  styleUrls: ["./eventos.component.css"]
})
export class EventosComponent implements OnInit {
  // Propriedades Primárias
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  isNew = true;
  bodyDeleteEvento = "";

  // Propriedade
  _filtroLista = "";

  constructor(
    private eventoService: EventoService,
    private fb: FormBuilder,
    private localeService: BsLocaleService
  ) {
    this.localeService.use("pt-br");
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista
      ? this.filtrarEventos(this.filtroLista)
      : this.eventos;
  }

  editarEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.isNew = false;
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  novoEvento(template: any) {
    this.openModal(template);
    this.isNew = true;
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeleteEvento = `Tem certeza que deseja excluir o evento ${evento.tema} ?`;
  }

  confirmarDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.getEventos();
      },
      error => {
        console.log(error);
      }
    );
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.isNew) {
        this.evento = Object.assign({}, this.registerForm.value);
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
          },
          er => {
            console.log(er);
          }
        );
      } else {
        this.evento = Object.assign(
          { id: this.evento.id },
          this.registerForm.value
        );
        this.eventoService.putEvento(this.evento).subscribe(
          (eventoEditado: Evento) => {
            template.hide();
            this.getEventos();
          },
          er => {
            console.log(er);
          }
        );
      }
    }
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: [
        "",
        [Validators.required, Validators.maxLength(50), Validators.minLength(4)]
      ],
      local: ["", Validators.required],
      dataEvento: ["", Validators.required],
      qtdPessoas: ["", [Validators.required, Validators.max(12000)]],
      imagemURL: ["", Validators.required],
      telefone: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]]
    });
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.eventoService.getAllEventos().subscribe(
      (_eventos: Evento[]) => {
        console.log(_eventos);
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error => console.log(error)
    );
  }
}

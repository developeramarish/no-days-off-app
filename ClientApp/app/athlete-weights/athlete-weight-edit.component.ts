import {
    Component,
    Input,
    OnInit,
    EventEmitter,
    Output,
    AfterViewInit,
    AfterContentInit,
    Renderer,
    ElementRef,
} from "@angular/core";

import {FormGroup,FormControl,Validators} from "@angular/forms";

@Component({
    templateUrl: "./athlete-weight-edit.component.html",
    styleUrls: [
        "../../styles/forms.css",
        "../../styles/edit.css",
        "./athlete-weight-edit.component.css"],
    selector: "ce-athlete-weight-edit"
})
export class AthleteWeightEditComponent {
    constructor() {
        this.tryToSave = new EventEmitter();
    }

    @Output()
    public tryToSave: EventEmitter<any>;

    private _athleteWeight: any = {};

    @Input("athleteWeight")
    public set athleteWeight(value) {
        this._athleteWeight = value;

        this.form.patchValue({
            id: this._athleteWeight.id,
            name: this._athleteWeight.name,
        });
    }
   
    public form = new FormGroup({
        id: new FormControl(0, []),
        name: new FormControl('', [Validators.required])
    });
}

import { CanDeactivate } from '@angular/router';
import { CreateOpportunityComponent } from './create-opportunity.component';
import { Injectable } from '@angular/core';

@Injectable()
export class OpportunityCanDeactivateGuardService implements CanDeactivate<CreateOpportunityComponent> {
  canDeactivate(component: CreateOpportunityComponent): boolean {
    if (component.createOpportunityForm.dirty) {
      return confirm('Are you sure you want to discard your changes?');
    }
    return true;
  }
}

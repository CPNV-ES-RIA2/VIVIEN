import { describe, it, expect } from 'vitest'

import { mount } from '@vue/test-utils'
import App from '../../App.vue'

describe('App', () => {
  it('renders properly', () => {
    // Given
    const wrapper = mount(App);

    // When

    // Expect
    expect(wrapper.text()).toContain('Number of labels');
    expect(wrapper.text()).toContain('Description');
    expect(wrapper.text()).toContain('Minimum confidence');
    expect(wrapper.text()).toContain('Confidence');
  })

  it('changes language properly', async () => {
    // Given
    const wrapper = mount(App);
    const languagePicker = wrapper.find('select');

    // When
    languagePicker.setValue('fr');
    await wrapper.vm.$nextTick();

    // Expect
    expect(wrapper.text()).toContain("Nombre d'étiquettes");
    expect(wrapper.text()).toContain("Déscription");
    expect(wrapper.text()).toContain("Confiance minimale");
    expect(wrapper.text()).toContain("Confiance");
  })

  it('displays results properly', () => {
    // TODO: Add testing logic here
  })
})

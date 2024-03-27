import { describe, it, expect } from 'vitest'

import { mount, flushPromises } from '@vue/test-utils'
import App from '../../App.vue'

describe('App', () => {
  it('renders properly', () => {
    // Given
    const wrapper = mount(App);

    // When

    // Expect
    expect(wrapper.text()).toContain('Maximum number of labels');
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
    expect(wrapper.text()).toContain("Nombre maximum d'étiquettes");
    expect(wrapper.text()).toContain("Déscription");
    expect(wrapper.text()).toContain("Confiance minimale");
    expect(wrapper.text()).toContain("Confiance");
  })

  it('displays results properly', async () => {
    // Given
    const wrapper = mount(App);
    const imagePicker = wrapper.find('#file-picker');
    var file = new File([0x0], "test-file.png", { type: "image/png" });

    // When
    Object.defineProperty(imagePicker.element, 'files', {
      value: [file],
      writable: false
    });
    imagePicker.trigger('change');
    await flushPromises();

    // Expect
    expect(wrapper.text()).toContain("cucumber");
    expect(wrapper.text()).toContain("potatoes");
    expect(wrapper.text()).toContain("carrots");
    expect(wrapper.text()).toContain("salad");
  })
})

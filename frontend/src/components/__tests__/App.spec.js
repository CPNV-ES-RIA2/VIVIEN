import { describe, it, expect } from 'vitest'

import { mount } from '@vue/test-utils'
import App from '../../App.vue'

describe('App', () => {
  it('renders properly', () => {
    const wrapper = mount(App)
    expect(wrapper.text()).toContain('Number of labels')
  })

  it('changes language properly', () => {
    const wrapper = mount(App)
    const languagePicker = wrapper.find('select')
    languagePicker.setValue('french')
    languagePicker.trigger('change')
    expect(wrapper.text()).toContain('Nombre de labels')
  })

  it('displays results properly', () => {
    // TODO: Add testing logic here
  })
})

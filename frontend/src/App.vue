<script setup>
import { ref, reactive } from 'vue';
import ImagePicker from './components/ImagePicker.vue';
import ResultsList from './components/ResultsList.vue';
import LanguagePicker from './components/LanguagePicker.vue';

var results = ref([{name: 'test', confidence: 0.1}, {name: 'test', confidence: 0.1}, {name: 'test', confidence: 0.1}, {name: 'test', confidence: 0.1}]);

function onAnalyzed(labels) {
  results.value = labels
}
</script>

<template>
<nav class="navbar navbar-light bg-light justify-content-between p-4 mb-3">
  <a class="navbar-brand">RIA2</a>
  <div class="form-inline">
    <LanguagePicker/>
  </div>
</nav>
  <div class="container">
    <ImagePicker @analyzed="onAnalyzed"/>
    <ResultsList :results="results"/>
  </div>
</template>

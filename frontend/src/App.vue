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
  <header>
    <LanguagePicker/>
  </header>
  <div class="wrapper">
    <ImagePicker @analyzed="onAnalyzed"/>
    <ResultsList :results="results"/>
  </div>
</template>

<style scoped>
</style>

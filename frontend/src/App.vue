<script setup>
import { ref, reactive } from 'vue';
import ResultsList from './components/ResultsList.vue';
import LanguagePicker from './components/LanguagePicker.vue';
import AnalysisOptions from './components/AnalysisOptions.vue';

const languages = ref(['english', 'french', 'german']);
var results = ref([{name: 'test', confidence: 0.1}, {name: 'test', confidence: 0.1}, {name: 'test', confidence: 0.1}, {name: 'test', confidence: 0.1}]);

function onFileUploading(files, xhr, formData) {
  xhr.onreadystatechange = () => {
    if (xhr.readyState == 4) {
      results.value = JSON.parse(xhr.response);
    }
  }
}

</script>

<template>
  <header>
    <LanguagePicker :languages="languages"/>
  </header>
  <div class="wrapper">
    <DropZone 
        method="POST"
        :maxFiles="Number(10000000000)"
        url="http://localhost:5224/Analyze"
        :uploadOnDrop="true"
        :multipleUpload="false"
        :parallelUpload="3"
        :acceptedFiles="['image']"
        @sending="onFileUploading"/>
      <AnalysisOptions />

      <ResultsList :results="results"/>
    </div>
</template>

<style scoped>
</style>

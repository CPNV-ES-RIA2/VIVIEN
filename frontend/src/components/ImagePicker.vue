<script setup>
import { ref, computed } from 'vue';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const imageTagsCountLabel = computed(() => {return t('imageTagsCountLabel')});
const minimumConfidenceLabel = computed(() => {return t('minimumConfidenceLabel')});

var imagePreviewSource = ref("");
var labelCount = ref(10);
var confidence = ref(0.9);

function onFileSelected(event) {
    try {
        imagePreviewSource.value = URL.createObjectURL(event.target.files[0]);
    } catch (ex) {
        console.error("Failed to create object URL");
    }
    let data = new FormData();
    data.append('file', event.target.files[0]);
    data.append('labelCount', labelCount.value);
    data.append('confidence', confidence.value);

    fetch('/Analyze', {
        method: 'POST',
        body: data
    }).then(
        (response) => response.json()
    ).then(
        (result) => emits('analyzed', result)
    ).catch(
        error => console.error("Could not analyze file: ", error)
    );
}

const emits = defineEmits([
    'analyzed'
])
</script>
<template>
    <form id="image-picker" class="mb-3">
        <div class="row mb-3">
            <img class="rounded mx-auto d-block img-fluid" :src="imagePreviewSource" @click="$refs.filepicker.click()">
            <input type="file" id="file-picker" name="file" accept="image/*" ref="filepicker" @change="onFileSelected"/>
        </div>
        <div class="row">
            <div class="col">
                <div class="row">
                    <label for="labelCount" class="col-9">{{ imageTagsCountLabel }}</label>
                    <input name="labelCount" type="number" step="1" max="10" min="1" v-model="labelCount" class="col-3">
                </div>
            </div>
            <div class="col">
                <div class="row">
                    <label for="confidence" class="col-9">{{ minimumConfidenceLabel }}</label>
                    <input name="confidence" type="number" step="0.001" max="1" min="0.001" v-model="confidence" class="col-3">
                </div>
            </div>
        </div>
    </form>
</template>

<style scoped>
    #file-picker {
        display: none;
    }
    #image-picker > div > img {
        position: relative;
        min-height: 200px;
        padding-top: 50%;
        border: 2px dashed #03A062;
        border-radius: 4px;
        padding: 4px;
    }
</style>
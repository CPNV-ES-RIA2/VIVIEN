<script setup>
import { computed } from 'vue';
import { useI18n } from 'vue-i18n';

const { locale, availableLocales } = useI18n();

function onUpdateLocale(event) {
    locale.value = event.target.value.toLowerCase();
}
</script>
<template>
    <select @change="onUpdateLocale($event)">
        <option v-for="locale in availableLocales" :value="locale">{{ locale }}</option>
    </select>
</template>